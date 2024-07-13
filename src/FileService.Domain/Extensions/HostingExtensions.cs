using FileService.Domain.Filters;
using FileService.Domain.Interfaces.Filters;
using FileService.Domain.Interfaces.Services;
using FileService.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileService.Domain.Extensions;

public static class HostingExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAppsService, AppsService>();
        services.AddScoped<IFoldersService, FoldersService>();
        services.AddScoped<IFilesService, FilesService>();

        services.AddScoped<ApiKeyAuthorizationFilter>();
        services.AddScoped<IApiKeyValidator,ApiKeyValidator>();
        return services;
    }
}
