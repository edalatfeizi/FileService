
using Microsoft.AspNetCore.Mvc;

namespace FileService.Domain.Filters;

public class AuthorizeApiKey : ServiceFilterAttribute
{
    public AuthorizeApiKey() : base(typeof(ApiKeyAuthorizationFilter))
    {
    }
}
