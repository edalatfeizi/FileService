﻿using FileService.Domain.Dtos.Res.File;
using FileService.Domain.Extensions;
using FileService.Domain.Interfaces.Repositories;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Resources;
using System.Net;

namespace FileService.Domain.Services;

public class FilesService : IFilesService
{
    private IFilesRepository fileRepo;
    private IAppsRepository appRepo;
    public FilesService(IFilesRepository fileRepository, IAppsRepository appRepository)
    {
        this.fileRepo = fileRepository;
        appRepo = appRepository;
    }

    public async Task<ApiResponse<DeleteFIleResDto>> DeleteFileAsync(string userId, int fileId)
    {
        var result = await fileRepo.DeleteFileAsync(userId,fileId);

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

    public async Task<ApiResponse<List<FileResDto>>> UploadFiles( int folderId, List<IFormFile> files)
    {
        //var app = await appRepo.GetAppByApiKeyAsync(apiKey);
        //if(app == null)
        //    return new ApiResponse<List<FileResDto>>((int) HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);

        var result = await fileRepo.SaveFilesAsync(folderId, files);
        if(result == null)
            return new ApiResponse<List<FileResDto>>((int)HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);

        return new ApiResponse<List<FileResDto>>(result.MapToFileResDtos());
    }

  
}
