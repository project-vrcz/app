using AsyncImageLoader.Loaders;
using Avalonia.Media.Imaging;

namespace VRCZ.App;

public class AppWebImageLoader(
    HttpClient httpClient,
    bool disposeHttpClient,
    string cacheFolder = "Cache/Images/") : DiskCachedWebImageLoader(httpClient, disposeHttpClient, cacheFolder)
{
    private readonly string _cacheFolder = cacheFolder;

    protected override Task<Bitmap?> LoadFromGlobalCache(string url)
    {
        var cacheFilePath = GetCacheFilePath(url);

        if (cacheFilePath is not null && File.Exists(cacheFilePath))
            return Task.FromResult<Bitmap?>(new Bitmap(cacheFilePath));

        return base.LoadFromGlobalCache(url);
    }

    protected override async Task SaveToGlobalCache(string url, byte[] imageBytes)
    {
        var cacheFilePath = GetCacheFilePath(url);

        if (cacheFilePath is not null)
        {
            var directoryName = Path.GetDirectoryName(cacheFilePath)!;

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            await File.WriteAllBytesAsync(cacheFilePath, imageBytes);
            return;
        }

        await base.SaveToGlobalCache(url, imageBytes);
    }

    public override async Task<Bitmap?> ProvideImageAsync(string url)
    {
        var uri = new Uri(url);

        if (!IsVRChatFiles(uri))
            return await base.ProvideImageAsync(url);

        return await LoadFromGlobalCache(url) ?? await base.ProvideImageAsync(url);
    }

    private string? GetCacheFilePath(string url)
    {
        var uri = new Uri(url);

        if (!IsVRChatFiles(uri))
            return null;

        var cacheKey = GetCacheKey(uri.AbsolutePath);

        return Path.Combine(_cacheFolder, cacheKey);
    }

    private string GetCacheKey(string urlPath)
    {
        var pathParts = urlPath.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
        pathParts = pathParts[3..];

        if (pathParts.Count == 2)
            pathParts.Add("file");

        pathParts.Insert(0, "vrchat-files");

        return Path.Join(pathParts.ToArray());
    }

    private bool IsVRChatFiles(Uri uri)
    {
        return uri.Host is "vrchat.com" or "api.vrchat.cloud" &&
               (uri.AbsolutePath.StartsWith("/api/1/image/") || uri.AbsolutePath.StartsWith("/api/1/file/"));
    }
}
