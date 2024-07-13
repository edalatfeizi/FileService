
using System.ComponentModel.DataAnnotations;

namespace FileService.Domain.Dtos.Request;

public class UserLoginReqDto
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}
