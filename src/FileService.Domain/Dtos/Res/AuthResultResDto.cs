
namespace FileService.Domain.Dtos.Response;

public record AuthResultResDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Result { get; set; } = false;

}
