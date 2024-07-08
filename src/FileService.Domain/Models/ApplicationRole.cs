

using Microsoft.AspNetCore.Identity;

namespace FileService.Domain.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
