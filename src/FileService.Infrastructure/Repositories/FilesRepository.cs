
using FileService.Domain.Entities;
using FileService.Infrastructure.Common;
using FileService.Infrastructure.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Repositories;

public class FilesRepository : IFilesRepository
{
    private readonly FileServiceDbContext dbContext;
    public FilesRepository(FileServiceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<AppFile>?> SaveFilesAsync(string userId, string appName, int folderId, List<IFormFile> files)
    {

        var newFiles = new List<AppFile>();
        var folder = await dbContext.Folders.Where(x => x.Id == folderId && x.IsActive).FirstOrDefaultAsync();
        if (folder == null)
            return null;
        foreach (var file in files)
        {

            var fileInfo = new FileInfo(file.FileName);
            var fileName = $"{DateTime.UtcNow.Ticks}{fileInfo.Extension}";
            var filePath = $@"{FileCommons.GetDirectoryPath(appName, folder.Name)}\{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var newFile = new AppFile
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                ParentFolderId = folder!.Id,
                Folder = folder,
                Path = filePath,
                CreatedBy = userId,
                ModifiedBy = userId,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                IsActive = true
            };
            newFiles.Add(newFile);
        }
        await dbContext.Files.AddRangeAsync(newFiles);
        await dbContext.SaveChangesAsync();

        return newFiles;
    }

    public async Task<(byte[], string, string)?> DownloadFileAsync(int fileId)
    {
        var file = await dbContext.Files.FindAsync(fileId);
        if (file == null) return null;
        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(file.Path, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        var fileBytes = await File.ReadAllBytesAsync(file.Path);
        return (fileBytes, contentType, Path.GetFileName(file.Path));

    }

    public async Task<(bool, string)?> DeleteFileAsync(string userId, int fileId)
    {

        try
        {
            var file = await dbContext.Files.Where(x => x.Id == fileId && x.IsActive).FirstOrDefaultAsync();
            if (file == null)
                return null;
            
            file.IsActive = false;
            file.ModifiedBy = userId;
            file.ModifiedAt = DateTime.UtcNow;

            File.Delete(file.Path);
            await dbContext.SaveChangesAsync();
            return (true, "");
        }
        catch (Exception ex)
        {
            return (true, $"An error occurred while deleting file: {ex.Message}");
        }

    }
}
