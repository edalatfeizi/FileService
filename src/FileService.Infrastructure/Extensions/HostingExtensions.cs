using FileService.Infrastructure.Data;
using FileService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FileService.Infrastructure.Extensions;

public static class HostingExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FileServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAppsRepository, AppsRepository>();
        services.AddScoped<IFilesRepository, FilesRepository>();
        services.AddScoped<IFoldersRepository, FoldersRepository>();
        return services;
    }
}
