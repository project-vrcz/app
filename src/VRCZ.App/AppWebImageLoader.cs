using Avalonia.Media.Imaging;
using VRCZ.Core.Services;

namespace VRCZ.App;

public class AppWebImageLoader(RemoteImageService remoteImageService)
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<Bitmap?> ProvideImageAsync(string url)
    {
        return await ProvideImageAsyncWithSize(url);
    }

    public async Task<Bitmap?> ProvideImageAsyncWithSize(string url, int? width = null, int? height = null)
    {
        var memoryStream = await remoteImageService.GetImageStreamAsync(url);

        if (height is not null && width is not null)
        {
            if (height >= width)
                return await Task.Run(() => Bitmap.DecodeToHeight(memoryStream, (int)height));

            return await Task.Run(() => Bitmap.DecodeToWidth(memoryStream, (int)width));
        }

        if (width is not null && height is null)
        {
            return await Task.Run(() => Bitmap.DecodeToWidth(memoryStream, (int)width));
        }

        if (height is not null && width is null)
        {
            return await Task.Run(() => Bitmap.DecodeToHeight(memoryStream, (int)height));
        }

        return await Task.Run(() => new Bitmap(memoryStream));
    }
}
