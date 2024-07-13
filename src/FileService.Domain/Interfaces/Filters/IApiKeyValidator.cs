

namespace FileService.Domain.Interfaces.Filters;

public interface IApiKeyValidator
{
   bool IsValid(string apiKey);
}
