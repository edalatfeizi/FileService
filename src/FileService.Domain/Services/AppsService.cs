﻿
using FileService.Domain.Dtos.Req.App;
using FileService.Domain.Dtos.Res.App;
using FileService.Domain.Dtos.Res.Folder;
using FileService.Domain.Extensions;
using FileService.Domain.Interfaces.Repositories;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Resources;
using System.Net;

namespace FileService.Domain.Services;

public class AppsService : IAppsService
{
    private readonly IAppsRepository appRepo;
    private readonly IFoldersRepository foldersRepo;
    public AppsService(IAppsRepository appRepository, IFoldersRepository foldersRepo)
    {
        appRepo = appRepository;
        this.foldersRepo = foldersRepo;
    }
    public async Task<ApiResponse<AppResDto>> AddAppAsync(string userId, AddAppReqDto dto)
    {
        var existApp = await appRepo.GetAppByNameAsync(dto.Name);
        if(existApp != null)
            return new ApiResponse<AppResDto>((int) HttpStatusCode.Conflict,ResponseMessages.ErrDuplicateAppName);

        var app = await appRepo.AddAppAsync(userId, dto.Name, dto.Description);

        var appFolder = await foldersRepo.AddFolderAsync(userId,app.Id,null,app.Name,dto.Description);

        return new ApiResponse<AppResDto>(app.MapToAppResDto());
    }

    public async Task<ApiResponse<AppResDto>> DeleteAppAsync(string userId, int appId)
    {
        var app = await appRepo.DeleteAppAsync(userId, appId);
        if(app == null) 
            return new ApiResponse<AppResDto>((int) HttpStatusCode.NotFound,ResponseMessages.ErrAppNotFound);
        return new ApiResponse<AppResDto>(app.MapToAppResDto());
    }

    public async Task<ApiResponse<List<AppResDto>>> GetAllAppsAsync()
    {
        var apps = await appRepo.GetAllAppsAsync();
        return new ApiResponse<List<AppResDto>>(apps.MapToAppResDtos());
    }

    public async Task<ApiResponse<AppResDto>> GetAppByApiKeyAsync(string apiKey)
    {
        var app = await appRepo.GetAppByApiKeyAsync(apiKey);
        if (app == null)
            return new ApiResponse<AppResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);
        return new ApiResponse<AppResDto>(app.MapToAppResDto());
    }

    public async Task<ApiResponse<AppResDto>> GetAppByIdAsync(int appId)
    {
        var app = await appRepo.GetAppByIdAsync(appId);
        if (app == null)
            return new ApiResponse<AppResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);
        return new ApiResponse<AppResDto>(app.MapToAppResDto());
    }

    public async Task<ApiResponse<string?>> GetAppOwnerIdAsync(string apiKey)
    {
        var appUser = await appRepo.GetAppUserByApiKeyAsync(apiKey);

        if(appUser == null)
            return new ApiResponse<string?>((int) HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound, null);

        return new ApiResponse<string?>(appUser.Id.ToString());
    }

    public async Task<ApiResponse<List<FolderResDto>>> GetFoldersAsync(int appId)
    {
        var appFolders = await appRepo.GetFoldersAsync(appId);
        if (appFolders == null)
            return new ApiResponse<List<FolderResDto>>((int)HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);
       
        return new ApiResponse<List<FolderResDto>>(appFolders.MapToFolderResDtos());
    }

    public async Task<ApiResponse<AppResDto>> RefreshAppApiKeyAsync(string userId, int appId)
    {
        var app = await appRepo.RefreshAppApiKeyAsync(userId, appId);
        if (app == null)
            return new ApiResponse<AppResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);
        return new ApiResponse<AppResDto>(app.MapToAppResDto());
    }

    public async Task<ApiResponse<AppResDto>> UpdateAppAsync(string userId, int appId, UpdateAppReqDto dto)
    {
        var app = await appRepo.UpdateAppAsync(userId, appId,dto.Name, dto.Description);
        if (app == null)
            return new ApiResponse<AppResDto>((int)HttpStatusCode.NotFound, ResponseMessages.ErrAppNotFound);
        return new ApiResponse<AppResDto>(app.MapToAppResDto());
    }
}
