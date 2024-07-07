namespace FileService.Domain.Interfaces.Repositories;

public interface IFileRepository
{
    Task<List<Entities.File>> SaveFilesAsync(string appId, List<IFormFile> files);
}