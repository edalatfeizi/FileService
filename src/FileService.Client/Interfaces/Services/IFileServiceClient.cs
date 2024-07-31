
using FileService.Client.Dtos.Res;

namespace FileService.Client.Interfaces.Services;

public interface IFileServiceClient
{
    Task<ApiResponse<FileResDto>> DownloadFileAsync(int fileId);
}
