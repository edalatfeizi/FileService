﻿namespace FileService.Domain.Interfaces.Repositories;

public interface IFilesRepository
{
    Task<List<Entities.AppFile>?> SaveFilesAsync(int folderId, List<IFormFile> files);
    Task<(byte[], string, string)?> DownloadFileAsync(int fileId);
    Task<(bool, string)?> DeleteFileAsync(string userId, int fileId);
}