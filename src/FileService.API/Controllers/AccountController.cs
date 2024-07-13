
using FileService.Domain.Dtos.Request;
using FileService.Domain.Dtos.Response;
using FileService.Domain.DTOs;
using FileService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Automation.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AuthResultResDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginReqDto dto)
    {
        var result = await _accountService.LoginAsync(dto);
        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AuthResultResDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterReqDto dto)
    {
        var result = await _accountService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpPost("refreshtoken")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AuthResultResDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenReqDto dto)
    {
        var result = await _accountService.RefreshTokenAsync(dto);
        return Ok(result);
    }
}
