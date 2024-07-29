
namespace FileService.Infrastructure.Common;

public static class FileCommons
{
    public static string GetCurrentDirectory()
    {
        var result = Directory.GetCurrentDirectory();
        return result;
    }
    public static string GetBasePath()
    {
        return @"E:\Data";
    }
    public static string GetDirectoryPath(string appName, string folderName)
    {
        var result = Path.Combine($@"{GetBasePath()}\{appName}\{folderName}");
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }

    public static string CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
    public static void RenameDirectory(string sourcePath,string destinationPath)
    {
        Directory.Move(sourcePath, destinationPath);
    }
    public static string GetFilePath(string appName, string folderName, string fileName)
    {
        var result = Path.Combine(GetDirectoryPath(appName,folderName), fileName);
        return result;
    }
}
