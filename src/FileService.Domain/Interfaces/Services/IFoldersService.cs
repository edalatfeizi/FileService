
using FileService.Domain.Dtos.Req.Folder;
using FileService.Domain.Dtos.Res.File;
using FileService.Domain.Dtos.Res.Folder;
using FileService.Domain.Entities;

namespace FileService.Domain.Interfaces.Services;

public interface IFoldersService
{
    Task<ApiResponse<FolderResDto>> AddFolderAsync(string apiKey, AddFolderReqDto dto);
    Task<ApiResponse<FolderResDto>> UpdateFolderAsync(int id, UpdateFolderReqDto dto);
    Task<ApiResponse<FolderResDto>> DeleteFolderAsync(int id);
    Task<ApiResponse<FolderResDto>> GetFolderByIdAsync(int id);
    Task<ApiResponse<List<FileResDto>>> GetFilesAsync(int id);
}
