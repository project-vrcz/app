using AsyncImageLoader;
using AsyncImageLoader.Loaders;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Ursa.Controls;

namespace VRCZ.App.Controls.AsyncImageLoader;

public class UrsaAvatarLoader
{
    private static readonly ParametrizedLogger? Logger;
    public static IAsyncImageLoader AsyncImageLoader { get; set; } = new RamCachedWebImageLoader();

    static UrsaAvatarLoader()
    {
        SourceProperty.Changed.AddClassHandler<Avatar>(OnSourceChanged);
        Logger = Avalonia.Logging.Logger.TryGet(LogEventLevel.Error, ImageLoader.AsyncImageLoaderLogArea);
    }

    private static async void OnSourceChanged(Avatar avatar, AvaloniaPropertyChangedEventArgs args)
    {
        var (oldValue, newValue) = args.GetOldAndNewValue<string?>();
        if (oldValue == newValue)
            return;

        SetIsLoading(avatar, true);

        Bitmap? bitmap = null;
        try
        {
            if (newValue is not null)
            {
                bitmap = await AsyncImageLoader.ProvideImageAsync(newValue);
            }
        }
        catch (Exception e)
        {
            Logger?.Log(nameof(UrsaAvatarLoader), nameof(UrsaAvatarLoader) + " image resolution failed: {0}", e);
        }

        if (GetSource(avatar) != newValue) return;
        avatar.Source = bitmap;

        SetIsLoading(avatar, false);
    }

    public static readonly AttachedProperty<string?> SourceProperty =
        AvaloniaProperty.RegisterAttached<ImageBrush, string?>("Source", typeof(ImageLoader));

    public static string? GetSource(Avatar element)
    {
        return element.GetValue(SourceProperty);
    }

    public static void SetSource(Avatar element, string? value)
    {
        element.SetValue(SourceProperty, value);
    }

    public static readonly AttachedProperty<bool> IsLoadingProperty =
        AvaloniaProperty.RegisterAttached<Avatar, bool>("IsLoading", typeof(ImageLoader));

    public static bool GetIsLoading(Avatar element)
    {
        return element.GetValue(IsLoadingProperty);
    }

    private static void SetIsLoading(Avatar element, bool value)
    {
        element.SetValue(IsLoadingProperty, value);
    }
}
