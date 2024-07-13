
using FileService.Domain.Entities;

namespace FileService.Domain.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<RefreshToken> AddUserRefreshTokenAsync(Guid userId, string token, string jwtId, bool isUsed, bool isRevoked, DateTime addedDate, DateTime expiryDate);
    Task<bool?> UpdateUserRefreshTokenAsync(Guid id, Guid userId, string token, string jwtId, bool isUsed, bool isRevoked, DateTime addedDate, DateTime expiryDate);
    Task<List<RefreshToken>> GeUserRefreshTokensAsync(Guid userId);
}
