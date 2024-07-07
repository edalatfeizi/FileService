
using Microsoft.AspNetCore.Identity;

namespace FileService.Domain.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
