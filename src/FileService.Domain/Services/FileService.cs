

using FileService.Domain.Interfaces.Repositories;
using FileService.Domain.Interfaces.Services;

namespace FileService.Domain.Services;

public class FileService : IFilesService
{
    private IFileRepository fileRepo;
    public FileService(IFileRepository fileRepository)
    {
        this.fileRepo = fileRepository;
    }
    public Task<ApiResponse<List<FileResDto>>> UploadFiles(string apiKey, List<IFormFile> files)
    {
        
    }
}
