
namespace FileService.Domain.Entities;

public class File : BaseEntity
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Path { get; set; }

    public Folder Folder { get; set; }
    [ForeignKey("Folder")]
    public int ParentFolderId { get; set; }
}
