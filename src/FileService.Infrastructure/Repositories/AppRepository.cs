
using FileService.Domain.Entities;
using FileService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Repositories;
public class AppRepository : IAppRepository
{
    private readonly FileServiceDbContext dbContext;
    public AppRepository(FileServiceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<App> AddAppAsync(string name, string description)
    {
        var app = new App
        {
            ApiKey = Guid.NewGuid().ToString().Replace("-", ""),
            Name = name,
            Description = description
        };
        await dbContext.Apps.AddAsync(app);
        await dbContext.SaveChangesAsync();
        return app;
    }

    public async Task<App?> DeleteAppAsync(int appId)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        if (app != null)
            app.IsActive = false;
        await dbContext.SaveChangesAsync();
        return app;
    }

    public async Task<List<App>> GetAllAppsAsync()
    {
        var apps = await dbContext.Apps.ToListAsync();
        return apps;
    }

    public async Task<App?> GetAppByApiKeyAsync(string apiKey)
    {
        var app = await dbContext.Apps.Where(x => x.ApiKey == apiKey && x.IsActive).FirstOrDefaultAsync();
        return app;
    }

    public async Task<App?> GetAppByIdAsync(int appId)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        return app;
    }

    public async Task<List<Folder>?> GetFoldersAsync(int appId)
    {
        var app = await dbContext.Apps.Where(x=> x.Id == appId && x.IsActive).Include(x => x.Folders).FirstOrDefaultAsync();
        return app != null ? app.Folders.ToList() : null;
    }

    public async Task<App?> RefreshAppApiKeyAsync(int appId)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        if (app != null)
            app.ApiKey = Guid.NewGuid().ToString().Replace("-","");
        await dbContext.SaveChangesAsync();
        return app;
    }

    public async Task<App?> UpdateAppAsync(int appId, string name, string description)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        if (app != null)
        {
            app.Name = name;
            app.Description = description;
        }
        await dbContext.SaveChangesAsync();
        return app;
    }
}
