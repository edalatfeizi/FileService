﻿
using FileService.Domain.Dtos.Res.File;

namespace FileService.Domain.Interfaces.Services;

public interface IFilesService
{
    Task<ApiResponse<List<FileResDto>>> UploadFiles(int folderId, List<IFormFile> files);
    Task<(byte[], string, string)?> DownloadFileAsync(int fileId);
    Task<ApiResponse<DeleteFIleResDto>> DeleteFileAsync(string userId,int fileId);
}