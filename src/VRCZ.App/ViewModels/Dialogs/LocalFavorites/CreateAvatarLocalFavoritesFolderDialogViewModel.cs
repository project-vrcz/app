using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using VRCZ.Core.Services.LocalFavorites;

namespace VRCZ.App.ViewModels.Dialogs.LocalFavorites;

public partial class CreateAvatarLocalFavoritesFolderDialogViewModel : ObservableValidator, IDialogContext
{
    private readonly AvatarLocalFavoritesService _localFavoritesService;

    public CreateAvatarLocalFavoritesFolderDialogViewModel(AvatarLocalFavoritesService localFavoritesService)
    {
        _localFavoritesService = localFavoritesService;

        ValidateAllProperties();
    }

    [MinLength(1)]
    [Required]
    [NotifyDataErrorInfo]
    [ObservableProperty]
    public partial string Name { get; set; } = "";

    [ObservableProperty] public partial string Description { get; set; } = "";

    [RelayCommand]
    private async Task Create()
    {
        ValidateAllProperties();

        if (HasErrors)
            return;

        await _localFavoritesService.CreateAvatarFavoritesFolderAsync(Name, Description);

        Close();
    }

    public void Close() => RequestClose?.Invoke(this, null);

    public event EventHandler<object?>? RequestClose;
}
