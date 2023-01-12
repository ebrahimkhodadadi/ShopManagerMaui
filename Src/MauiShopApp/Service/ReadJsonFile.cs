
using System.Reflection;

namespace MauiShopApp.Service;

public static class ReadJsonFile
{
    public static async Task<T> ReadJson<T>(string fileName = "appsettings.json")
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var output = JsonSerializer.Deserialize<T>(json);
        return output;
    }
}
