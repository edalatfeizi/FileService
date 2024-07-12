using FileService.Domain.Entities;

namespace FileService.Domain.Interfaces.Repositories;

public interface IAppRepository
{
    Task<App?> GetAppByApiKeyAsync(string apiKey);
}
