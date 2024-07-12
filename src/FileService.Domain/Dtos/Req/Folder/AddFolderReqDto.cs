
namespace FileService.Domain.Dtos.Req.Folder;

public class AddFolderReqDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int? ParentFolderId { get; set; }
}
