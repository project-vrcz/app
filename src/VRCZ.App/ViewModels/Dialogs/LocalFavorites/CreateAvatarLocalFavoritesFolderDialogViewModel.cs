using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irihi.Avalonia.Shared.Contracts;
using VRCZ.Core.Services.LocalFavorites;

namespace VRCZ.App.ViewModels.Dialogs.LocalFavorites;

public partial class CreateAvatarLocalFavoritesFolderDialogViewModel(AvatarLocalFavoritesService localFavoritesService)
    : ObservableValidator, IDialogContext
{
    [MinLength(1)]
    [Required]
    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private string _description = "";


    [RelayCommand]
    private async Task Create()
    {
        ValidateAllProperties();

        if (HasErrors)
            return;

        await localFavoritesService.CreateAvatarFavoritesFolderAsync(Name, Description);

        Close();
    }

    public void Close() => RequestClose?.Invoke(this, null);

    public event EventHandler<object?>? RequestClose;
}
