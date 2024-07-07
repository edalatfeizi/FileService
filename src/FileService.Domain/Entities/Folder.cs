
namespace FileService.Domain.Entities;

public class Folder : BaseEntity
{
    public string  Name { get; set; }
    public string  Description { get; set; }
    public int? ParentFolderId { get; set; }

    public ICollection<File> Files { get; set; }
}
