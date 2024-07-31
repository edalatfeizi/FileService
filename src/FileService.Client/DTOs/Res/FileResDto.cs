
namespace FileService.Client.Dtos.Res;

public record FileResDto(byte[] content,string contentType, string fileName);
