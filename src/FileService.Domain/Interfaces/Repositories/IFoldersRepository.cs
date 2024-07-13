
using FileService.Domain.Entities;

namespace FileService.Domain.Interfaces.Repositories;

public interface IFoldersRepository
{
    Task<Folder> AddFolderAsync(string userId, int parentAppId, int? parentFolderId, string name, string description);
    Task<Folder?> UpdateFolderAsync(string userId, int id, string name, string description);
    Task<Folder?> DeleteFolderAsync(string userId, int id);
    Task<Folder?> GetFolderByIdAsync(int id);
    Task<List<Entities.AppFile>> GetFilesAsync(int id);   
}
