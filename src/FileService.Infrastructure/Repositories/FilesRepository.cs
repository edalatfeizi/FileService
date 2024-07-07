
using FileService.Infrastructure.Data;

namespace FileService.Infrastructure.Repositories;

public class FilesRepository : IFileRepository
{
    private readonly FileServiceDbContext dbContext;
    public FilesRepository(FileServiceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public Task<List<Domain.Entities.File>> SaveFilesAsync(string appId, List<IFormFile> files)
    {
        throw new NotImplementedException();
    }
}
