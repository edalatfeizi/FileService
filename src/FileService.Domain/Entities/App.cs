

using FileService.Domain.Models;

namespace FileService.Domain.Entities;

public class App : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ApiKey { get; set; }

    public ApplicationUser User { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
}
