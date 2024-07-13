
namespace FileService.Domain.Dtos.Res.File;

public record DeleteFIleResDto
{
    public bool IsDeleted { get; set; }
    public string Error { get; set; }
}
