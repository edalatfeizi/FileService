
using System.ComponentModel.DataAnnotations;

namespace FileService.Domain.Dtos.Request;

public class TokenReqDto
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}
