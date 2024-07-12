﻿namespace FileService.Domain.Interfaces.Repositories;

public interface IFilesRepository
{
    Task<List<Entities.File>?> SaveFilesAsync(string appName, int folderId, List<IFormFile> files);
    Task<(byte[], string, string)?> DownloadFileAsync(int fileId);
    Task<(bool, string)?> DeleteFileAsync(int fileId);
}