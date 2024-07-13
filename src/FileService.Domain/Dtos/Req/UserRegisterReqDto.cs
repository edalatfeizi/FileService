
using System.ComponentModel.DataAnnotations;

namespace FileService.Domain.Dtos.Request;

public class UserRegisterReqDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}
