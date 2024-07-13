using FileService.Domain.Dtos.Res.App;
using FileService.Domain.Entities;
namespace FileService.Domain.Extensions;

public static class AppExtensions
{
    public static AppResDto MapToAppResDto(this App app)
    {
        var appDto = new AppResDto
        {
            Id = app.Id,
            ApiKey = app.ApiKey,
            Name = app.Name,
            Description = app.Description
        };

        return appDto; 
    }

    public static List<AppResDto> MapToAppResDtos(this List<App> apps)
    {
        var appResDtos = new List<AppResDto>();
        foreach (var app in apps)
        {
            var appDto = app.MapToAppResDto();
            appResDtos.Add(appDto);
        }
        return appResDtos;
    }
}
