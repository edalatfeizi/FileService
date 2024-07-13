
using FileService.Domain.Entities;
using FileService.Domain.Models;
using FileService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Repositories;
public class AppsRepository : IAppsRepository
{
    private readonly FileServiceDbContext dbContext;
    public AppsRepository(FileServiceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<App> AddAppAsync(string userId, string name, string description)
    {
        var app = new App
        {
            ApiKey = Guid.NewGuid().ToString().Replace("-", ""),
            Name = name,
            Description = description,
            CreatedBy = userId,
            ModifiedBy = userId,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            UserId = Guid.Parse(userId),
            IsActive = true,
        };
        await dbContext.Apps.AddAsync(app);
        await dbContext.SaveChangesAsync();
        return app;
    }

    public async Task<App?> DeleteAppAsync(string userId, int appId)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        if (app != null)
        {
            app.IsActive = false;
            app.ModifiedAt = DateTime.UtcNow;
            app.ModifiedBy = userId;
            await dbContext.SaveChangesAsync();
        }
       
        return app;
    }

    public async Task<List<App>> GetAllAppsAsync()
    {
        var apps = await dbContext.Apps.ToListAsync();
        return apps;
    }

    public App? GetAppByApiKey(string apiKey)
    {
        var app =  dbContext.Apps.Where(x => x.ApiKey == apiKey && x.IsActive).FirstOrDefault();
        return app;
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

    public async Task<ApplicationUser?> GetAppUserByApiKeyAsync(string apiKey)
    {
        var app = await dbContext.Apps.Where(x => x.ApiKey == apiKey && x.IsActive).Include(x=> x.User).FirstOrDefaultAsync();

        return app != null ? app.User : null;
    }

    public async Task<List<Folder>?> GetFoldersAsync(int appId)
    {
        var app = await dbContext.Apps.Where(x=> x.Id == appId && x.IsActive).Include(x => x.Folders).FirstOrDefaultAsync();
        return app != null ? app.Folders.ToList() : null;
    }

    public async Task<App?> RefreshAppApiKeyAsync(string userId, int appId)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        if (app != null)
        {
            app.ApiKey = Guid.NewGuid().ToString().Replace("-", "");
            app.ModifiedAt = DateTime.UtcNow;
            app.ModifiedBy = userId;
            await dbContext.SaveChangesAsync();
        }
           
        return app;
    }

    public async Task<App?> UpdateAppAsync(string userId, int appId, string name, string description)
    {
        var app = await dbContext.Apps.Where(x => x.Id == appId && x.IsActive).FirstOrDefaultAsync();
        if (app != null)
        {
            app.Name = name;
            app.Description = description;
            app.ModifiedAt = DateTime.UtcNow;
            app.ModifiedBy = userId;
            await dbContext.SaveChangesAsync();

        }
        return app;
    }
}
