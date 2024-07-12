
namespace FileService.Domain.Dtos.Res.Folder;

public class FolderResDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ParentFolderId { get; set; }
}
