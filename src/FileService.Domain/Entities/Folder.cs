
namespace FileService.Domain.Entities;

public class Folder : BaseEntity
{
    public string  Name { get; set; }
    public string  Description { get; set; }
    public int? ParentFolderId { get; set; }

    public App App { get; set; }
    [ForeignKey("App")]
    public int ParentAppId { get; set; }
    public ICollection<AppFile> Files { get; set; }
}
