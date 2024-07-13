namespace FileService.API.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        var identity = context.User.Identities.First();
        var userId = Guid.Parse(identity.Claims.Where(x => x.Type == "Id").First().Value!.ToString());
        return userId;
    }
    public static string GetApiKey(this HttpContext context)
    {
        var apiKey = context.Request.Headers["X-API-KEY"];
        return apiKey.ToString() ?? "";
    }
}
