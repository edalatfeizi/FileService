using FileService.Domain.Entities;

namespace FileService.Domain.Interfaces.Repositories;

public interface IAppsRepository
{
    Task<App?> GetAppByApiKeyAsync(string apiKey);
    Task<App?> GetAppByIdAsync(int appId);
    Task<App> AddAppAsync(string name, string description);
    Task<App?> UpdateAppAsync(int appId, string name, string description);
    Task<App?> DeleteAppAsync(int appId);
    Task<App?> RefreshAppApiKeyAsync(int appId);
    Task<List<Folder>?> GetFoldersAsync(int appId);
    Task<List<App>> GetAllAppsAsync();
}

