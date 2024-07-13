
using FileService.Domain.Interfaces.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileService.Domain.Filters;

public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly IApiKeyValidator apiKeyValidator;
    public ApiKeyAuthorizationFilter(IApiKeyValidator apiKeyValidator)
    {
        this.apiKeyValidator = apiKeyValidator;
    }
    public  void OnAuthorization(AuthorizationFilterContext context)
    {
        string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName].ToString() ?? "";
        var result =  apiKeyValidator.IsValid(apiKey);
        if (!result)
            context.Result = new UnauthorizedResult();
    }
}
