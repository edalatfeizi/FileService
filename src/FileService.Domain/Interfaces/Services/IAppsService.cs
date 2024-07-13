
using FileService.Domain.Dtos.Req.App;
using FileService.Domain.Dtos.Res.App;
using FileService.Domain.Dtos.Res.Folder;
namespace FileService.Domain.Interfaces.Services;

public interface IAppsService
{
    Task<ApiResponse<AppResDto>> GetAppByApiKeyAsync(string apiKey);
    Task<ApiResponse<AppResDto>> GetAppByIdAsync(int appId);
    Task<ApiResponse<AppResDto>> AddAppAsync(string userId, AddAppReqDto dto);
    Task<ApiResponse<AppResDto>> UpdateAppAsync(string userId, int appId, UpdateAppReqDto dto);
    Task<ApiResponse<AppResDto>> DeleteAppAsync(string userId, int appId);
    Task<ApiResponse<AppResDto>> RefreshAppApiKeyAsync(string userId, int appId);
    Task<ApiResponse<List<FolderResDto>>> GetFoldersAsync(int appId);
    Task<ApiResponse<List<AppResDto>>> GetAllAppsAsync();
}
