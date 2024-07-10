using FileService.Infrastructure.Data;
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

        return services;
    }
}
