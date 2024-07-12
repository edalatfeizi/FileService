﻿
using FileService.Domain.Dtos.Req.Folder;
using FileService.Domain.Dtos.Res.File;
using FileService.Domain.Dtos.Res.Folder;
using FileService.Domain.Extensions;
using FileService.Domain.Interfaces.Repositories;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Resources;
using System.Net;

namespace FileService.Domain.Services;

public class FoldersService : IFoldersService
{
    private readonly IFoldersRepository folderRepo;
    private readonly IAppsRepository appRepo;
    public FoldersService(IFoldersRepository folderRepository, IAppsRepository appRepository)
    {
        folderRepo = folderRepository;
        appRepo = appRepository;
    }

    public async Task<ApiResponse<FolderResDto>> AddFolderAsync(string apiKey, AddFolderReqDto dto)
    {
        var parentApp = await appRepo.GetAppByApiKeyAsync(apiKey);
        int parentFolderId = 0;
        if (parentApp == null)
            return new ApiResponse<FolderResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);
        
        if(dto.ParentFolderId != null)
        {
            var folderId = dto.ParentFolderId;
            var parentFolder = await folderRepo.GetFolderByIdAsync((int)folderId);
            if(parentFolder == null)
                return new ApiResponse<FolderResDto>((int) HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);
        }

        var folder = await folderRepo.AddFolderAsync(parentApp.Id, parentFolderId, dto.Name, dto.Description);
        return new ApiResponse<FolderResDto>(folder.MapToFolderResDto());
    }

    public async Task<ApiResponse<FolderResDto>> DeleteFolderAsync(int id)
    {
        var result = await folderRepo.DeleteFolderAsync(id);
        if(result == null)
            return new ApiResponse<FolderResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);
        return new ApiResponse<FolderResDto>(result.MapToFolderResDto());
    }

    public async Task<ApiResponse<List<FileResDto>>> GetFilesAsync(int id)
    {
        var result = await folderRepo.GetFilesAsync(id);
        if (result == null)
            return new ApiResponse<List<FileResDto>> ((int)HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);

        return new ApiResponse<List<FileResDto>>(result.MapToFileResDtos());
    }

    public async Task<ApiResponse<FolderResDto>> GetFolderByIdAsync(int id)
    {
        var result = await folderRepo.GetFolderByIdAsync(id);
        if (result == null)
            return new ApiResponse<FolderResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);
        return new ApiResponse<FolderResDto>(result.MapToFolderResDto());
    }

    public async Task<ApiResponse<FolderResDto>> UpdateFolderAsync(int id, UpdateFolderReqDto dto)
    {
        var result = await folderRepo.UpdateFolderAsync(id,dto.Name,dto.Description);
        if (result == null)
            return new ApiResponse<FolderResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrFolderNotFound);
        return new ApiResponse<FolderResDto>(result.MapToFolderResDto());
    }
}