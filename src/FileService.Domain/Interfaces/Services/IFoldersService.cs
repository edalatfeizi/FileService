
using FileService.Domain.Dtos.Req.Folder;
using FileService.Domain.Dtos.Res.File;
using FileService.Domain.Dtos.Res.Folder;

namespace FileService.Domain.Interfaces.Services;

public interface IFoldersService
{
    Task<ApiResponse<FolderResDto>> AddFolderAsync(string userId, string apiKey, AddFolderReqDto dto);
    Task<ApiResponse<FolderResDto>> UpdateFolderAsync(string userId, int folderId, UpdateFolderReqDto dto);
    Task<ApiResponse<FolderResDto>> DeleteFolderAsync(string userId, int folderId);
    Task<ApiResponse<FolderResDto>> GetFolderByIdAsync(int folderId);
    Task<ApiResponse<List<FileResDto>>> GetFilesAsync(int folderId);
}
