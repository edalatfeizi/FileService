using FileService.Domain.Dtos.Res.File;
using FileService.Domain.Extensions;
using FileService.Domain.Interfaces.Repositories;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Resources;
using System.Net;

namespace FileService.Domain.Services;

public class FileService : IFilesService
{
    private IFileRepository fileRepo;
    private IAppRepository appRepo;
    public FileService(IFileRepository fileRepository, IAppRepository appRepository)
    {
        this.fileRepo = fileRepository;
        appRepo = appRepository;
    }

    public async Task<ApiResponse<DeleteFIleResDto>> DeleteFileAsync(int fileId)
    {
        var result = await fileRepo.DeleteFileAsync(fileId);

        if (result == null)
            return new ApiResponse<DeleteFIleResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrFileNotFound);

        return new ApiResponse<DeleteFIleResDto>(new DeleteFIleResDto { IsDeleted = result.Value.Item1, Error = result.Value.Item2 });
    }

    public async Task<(byte[], string, string)?> DownloadFileAsync(int fileId)
    {
        var result = await fileRepo.DownloadFileAsync(fileId);
        if(result == null) 
            return null;

        return result;
    }

    public async Task<ApiResponse<List<FileResDto>>> UploadFiles(string apiKey, int folderId, List<IFormFile> files)
    {
        var app = await appRepo.GetAppByApiKeyAsync(apiKey);
        if(app == null)
            return new ApiResponse<List<FileResDto>>((int) HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);

        var result = await fileRepo.SaveFilesAsync(app.Name, folderId, files);
        if(result == null)
            return new ApiResponse<List<FileResDto>>((int)HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);

        return new ApiResponse<List<FileResDto>>(result.MapToFileResDtos());
    }

  
}
