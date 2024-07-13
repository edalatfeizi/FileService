
using FileService.Domain.Entities;
using FileService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Repositories;

public class FoldersRepository : IFoldersRepository
{
    private readonly FileServiceDbContext dbContext;
    public FoldersRepository(FileServiceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Folder> AddFolderAsync(string userId, int parentAppId, int? parentFolderId, string name, string description)
    {
        var parentApp = await dbContext.Apps.FindAsync(parentAppId);
        var parentFolder = await dbContext.Folders.FindAsync(parentFolderId);

        var folder = new Folder
        {
            App = parentApp!,
            ParentAppId = parentAppId,
            ParentFolderId = parentFolderId,
            Name = name,
            Description = description,
            CreatedBy = userId,
            ModifiedBy = userId,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            IsActive = true
        };

        await dbContext.Folders.AddAsync(folder);
        await dbContext.SaveChangesAsync();

        return folder;
    }

    public async Task<Folder?> DeleteFolderAsync(string userId, int id)
    {
        var folder = await dbContext.Folders.FindAsync(id);
        if (folder != null)
        {
            folder.IsActive = false;
            folder.ModifiedBy = userId;
            folder.ModifiedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

        }
        return folder;
    }

    public async Task<List<AppFile>> GetFilesAsync(int id)
    {
        var folder = await dbContext.Folders.Where(x => x.Id == id && x.IsActive).Include(x => x.Files).FirstOrDefaultAsync();
        if (folder != null)
            return folder.Files.ToList();

        return new List<AppFile>();
    }

    public async Task<Folder?> GetFolderByIdAsync(int id)
    {
        var folder = await dbContext.Folders.FindAsync(id);
        return folder;
    }

    public async Task<Folder?> UpdateFolderAsync(string userId, int folderId, string name, string description)
    {
        var folder = await dbContext.Folders.FindAsync(folderId);
        if (folder != null)
        {
            folder.Name = name;
            folder.Description = description;
            folder.ModifiedBy = userId;
            folder.ModifiedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

        }
        return folder;
    }
}
