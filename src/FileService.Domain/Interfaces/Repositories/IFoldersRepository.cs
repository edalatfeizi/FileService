
using FileService.Domain.Entities;

namespace FileService.Domain.Interfaces.Repositories;

public interface IFoldersRepository
{
    Task<Folder> AddFolderAsync(int parentAppId, int? parentFolderId, string name, string description);
    Task<Folder?> UpdateFolderAsync(int id, string name, string description);
    Task<Folder?> DeleteFolderAsync(int id);
    Task<Folder?> GetFolderByIdAsync(int id);
    Task<List<Entities.File>> GetFilesAsync(int id);   
}
