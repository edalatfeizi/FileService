using FileService.Domain.Dtos.Res.File;
namespace FileService.Domain.Extensions;

public static class FileExtensions
{
    public static FileResDto MapToFileResDto(this Entities.File file)
    {
        var fileDto = new FileResDto
        {
            Id = file.Id,
            Name = file.Name,
            ContentType = file.ContentType,
            Size = file.Size,
            Path = file.Path,
        };

        return fileDto; 
    }

    public static List<FileResDto> MapToFileResDtos(this List<Entities.File> files)
    {
        var fileResDtos = new List<FileResDto>();
        foreach (var file in files)
        {
            var fileDto = file.MapToFileResDto();
            fileResDtos.Add(fileDto);
        }
        return fileResDtos;
    }
}
