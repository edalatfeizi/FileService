using FileService.Domain.Dtos.Res.Folder;
using FileService.Domain.Entities;

namespace FileService.Domain.Extensions;

public static class FolderExtensions
{
    public static FolderResDto MapToFolderResDto(this Folder folder)
    {
        var folderDto = new FolderResDto
        {
            Id = folder.Id,
            Name = folder.Name,
            Description = folder.Description,
            ParentFolderId = folder.ParentFolderId ?? 0
        };

        return folderDto; 
    }

    public static List<FolderResDto> MapToFolderResDtos(this List<Folder> folders)
    {
        var folderResDtos = new List<FolderResDto>();
        foreach (var folder in folders)
        {
            var folderDto = folder.MapToFolderResDto();
            folderResDtos.Add(folderDto);
        }
        return folderResDtos;
    }
}
