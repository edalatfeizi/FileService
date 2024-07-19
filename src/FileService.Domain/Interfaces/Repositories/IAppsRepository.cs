using FileService.Domain.Entities;
using FileService.Domain.Models;

namespace FileService.Domain.Interfaces.Repositories;

public interface IAppsRepository
{
    Task<App?> GetAppByApiKeyAsync(string apiKey);
    App? GetAppByApiKey(string apiKey);
    Task<App?> GetAppByIdAsync(int appId);
    Task<App?> GetAppByNameAsync(string name);
    Task<App> AddAppAsync(string userId, string name, string description);
    Task<App?> UpdateAppAsync(string userId, int appId, string name, string description);
    Task<App?> DeleteAppAsync(string userId, int appId);
    Task<App?> RefreshAppApiKeyAsync(string userId, int appId);
    Task<List<Folder>?> GetFoldersAsync(int appId);
    Task<List<App>> GetAllAppsAsync();
    Task<ApplicationUser?> GetAppUserByApiKeyAsync(string apiKey);
}

