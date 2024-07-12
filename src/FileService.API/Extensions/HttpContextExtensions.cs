namespace FileService.API.Extensions;

public static class HttpContextExtensions
{
    public static string GetApiKey(this HttpContext context)
    {
        var apiKey = context.Request.Headers["X-API-KEY"];
        return apiKey.ToString() ?? "";
    }
}
