
using FileService.Domain.Dtos.Request;
using FileService.Domain.Dtos.Response;
using FileService.Domain.Interfaces.Repositories;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Models;
using FileService.Domain.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace FileService.Domain.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly TokenValidationParameters _tokenValidationParameters;
    public AccountService(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, IConfiguration configuration, TokenValidationParameters tokenValidationParameters)
    {
        _accountRepo = accountRepository;
        _userManager = userManager;
        _configuration = configuration;
        _tokenValidationParameters = tokenValidationParameters;

    }
    public async Task<ApiResponse<AuthResultResDto>> LoginAsync(UserLoginReqDto dto)
    {
        var existUser = await _userManager.FindByEmailAsync(dto.Email);

        //check user exist
        if (existUser == null)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.InvalidCredentials);

        var isValidCredentials = await _userManager.CheckPasswordAsync(existUser, dto.Password);

        //check password correctness
        if (!isValidCredentials)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.InvalidCredentials);

        var result = await GenerateJwtToken(existUser);

        return new ApiResponse<AuthResultResDto>((result));
    }

    public async Task<ApiResponse<AuthResultResDto>> RegisterAsync(UserRegisterReqDto dto)
    {
        var existUser = await _userManager.FindByEmailAsync(dto.Email);

        //check user exist
        if (existUser != null)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.EmailAlreadyExist);

        //create new user
        var newUser = new ApplicationUser()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email,
        };

        var isUserCreated = await _userManager.CreateAsync(newUser, dto.Password);
        if (isUserCreated.Succeeded)
        {
            var authResult = await GenerateJwtToken(newUser);
            return new ApiResponse<AuthResultResDto>(authResult);
        }

        var errors = new StringBuilder();
        foreach (var error in isUserCreated.Errors)
            errors.Append(error.Description);

        return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, errors.ToString());

    }

    public async Task<ApiResponse<AuthResultResDto>> RefreshTokenAsync(TokenReqDto dto)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        _tokenValidationParameters.ValidateLifetime = false; //true for production
        var tokenInVerification = jwtTokenHandler.ValidateToken(dto.Token, _tokenValidationParameters, out var validatedToken);

        //if received token is generated with a different algorithm than HmacSha256, means token has been manipulated.
        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        {
            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

            if (result == false)
                return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.InvalidTokens);
        }

        /*
        * each jwt token will be expired for example 15 min after it is generated 
        * so when user wants to refresh it's token, first we check user's jwt token expiration time   
        * first we add that 15 min to the base unix time and get a datetime indicating expiry time in datetime format 2023/10/26:16:45:30
        * and then compare that with current datetime for example 2023/10/26:17:00:30 if current time is greater than token expire time, means that token is expired.
       */
        var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);

        var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

        if (DateTime.UtcNow > expiryDate)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.ExpiredTokens);

        var userId = tokenInVerification.Claims.FirstOrDefault(x => x.Type == "Id");
        if (userId == null)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.InvalidTokens);

        var storedTokens = await _accountRepo.GeUserRefreshTokensAsync(Guid.Parse(userId.Value));
        var exitRefreshToken = storedTokens.FirstOrDefault(x => x.Token == dto.RefreshToken);
        if (exitRefreshToken == null || exitRefreshToken.IsUsed || exitRefreshToken.IsRevoked)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.InvalidTokens);

        //when generate jwt token we set a unique id to it
        //when user wants to refresh that token we check it's id with stored token's id and if they are different means token is invalid
        var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;
        if (exitRefreshToken.JwtId != jti)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.InvalidTokens);

        //check if stored refresh token's expiry time is greater than current time, 
        //if it is, means refresh token is expired and user should login to system again
        if (exitRefreshToken.ExpiryDate < DateTime.UtcNow)
            return new ApiResponse<AuthResultResDto>((int)HttpStatusCode.BadRequest, ResponseMessages.ExpiredTokens);


        exitRefreshToken.IsUsed = true;
        await _accountRepo.UpdateUserRefreshTokenAsync(exitRefreshToken.Id, exitRefreshToken.UserId, exitRefreshToken.Token, exitRefreshToken.JwtId, exitRefreshToken.IsUsed, exitRefreshToken.IsRevoked, exitRefreshToken.AddedDate, exitRefreshToken.ExpiryDate);
        var user = await _userManager.FindByIdAsync(exitRefreshToken.UserId.ToString());

        var authResult = await GenerateJwtToken(user!);

        return new ApiResponse<AuthResultResDto>(authResult);


    }


    private async Task<AuthResultResDto> GenerateJwtToken(ApplicationUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString())
            }),
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        var refreshToken = new RefreshTokenResDto
        {
            JwtId = token.Id,
            Token = RandomStringGenerator(jwtToken.Length),
            AddedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6),
            IsRevoked = false,
            IsUsed = false,
            UserId = Guid.Parse(user.Id.ToString()),
        };

        await _accountRepo.AddUserRefreshTokenAsync(refreshToken.UserId, refreshToken.Token, refreshToken.JwtId, refreshToken.IsUsed, refreshToken.IsRevoked, refreshToken.AddedDate, refreshToken.ExpiryDate);

        return new AuthResultResDto() { Result = true, Token = jwtToken, RefreshToken = refreshToken.Token };


    }

    private string RandomStringGenerator(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(chars.Length)]).ToArray());
    }

    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeVal;
    }
}
