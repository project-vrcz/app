using AsyncImageLoader;
using AsyncImageLoader.Loaders;
using VRCZ.Core.Utils;

namespace VRCZ.App.Services;

public class AsyncImageLoaderFactory(HttpClient httpClient)
{
    public IAsyncImageLoader Create()
    {
        return new AppWebImageLoader(httpClient, false, ProfileStorageUtils.GetImageCacheRootPath());
    }
}
