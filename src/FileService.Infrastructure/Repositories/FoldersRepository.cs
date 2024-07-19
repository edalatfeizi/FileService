
using FileService.Domain.Entities;
using FileService.Infrastructure.Common;
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
        var parentApp = await dbContext.Apps.Where(x=> x.Id == parentAppId && x.IsActive).Include(x=> x.Folders).FirstOrDefaultAsync();
        var parentFolder = await dbContext.Folders.FindAsync(parentFolderId);
        var folderPath = "";
        if(parentFolder != null)
        {
            folderPath = FileCommons.CreateDirectory($@"{parentFolder.Path}\{name}");

        }
        else if (parentApp!.Folders.Count > 0)
        {
            folderPath = FileCommons.CreateDirectory($@"{FileCommons.GetBasePath()}\{parentApp!.Name}\{name}");
        }
        else
        {
            folderPath = FileCommons.CreateDirectory($@"{FileCommons.GetBasePath()}\{name}");

        }
        var existFolder = await dbContext.Folders.Where(x=> x.Path == folderPath && x.IsActive).FirstOrDefaultAsync();
        if(existFolder == null)
        {
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
                Path = folderPath,
                IsActive = true
            };
            await dbContext.Folders.AddAsync(folder);
            await dbContext.SaveChangesAsync();
            return folder;
        }
 

        return existFolder;
    }

    public async Task<Folder?> DeleteFolderAsync(string userId, int id)
    {
        var folder = await dbContext.Folders.FindAsync(id);
        if (folder != null)
        {

            string[] allfilePaths = Directory.GetFiles(folder.Path, "*.*", SearchOption.AllDirectories);
            foreach (var filePath in allfilePaths)
            {
                filePath.Replace(@"\\", @"\");
                var file = await dbContext.Files.Where(x => x.Path == filePath && x.IsActive).FirstOrDefaultAsync();
                if(file != null)
                {
                    file.IsActive = false;
                    file.ModifiedBy = userId;
                    file.ModifiedAt = DateTime.UtcNow;
                }
            }

            var dir = new DirectoryInfo(folder.Path);

            dir.Delete(true);

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
