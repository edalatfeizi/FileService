
namespace FileService.Infrastructure.Common;

public static class FileCommons
{
    public static string GetCurrentDirectory()
    {
        var result = Directory.GetCurrentDirectory();
        return result;
    }
    public static string GetDirectoryPath(string appName, string folderName)
    {
        var result = Path.Combine($@"D:\Data\{appName}\{folderName}");
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }
    public static string GetFilePath(string appName, string folderName, string fileName)
    {
        var result = Path.Combine(GetDirectoryPath(appName,folderName), fileName);
        return result;
    }
}
