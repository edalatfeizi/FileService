
using FileService.Domain.Interfaces.Filters;
using FileService.Domain.Interfaces.Repositories;

namespace FileService.Domain.Filters;

public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IAppsRepository appRepo;
    public ApiKeyValidator(IAppsRepository appsRepository)
    {
        this.appRepo = appsRepository;
    }
    public bool IsValid(string apiKey)
    {
        var app =  appRepo.GetAppByApiKey(apiKey);
        if (app == null)
            return false;
        return true;
    }
}
