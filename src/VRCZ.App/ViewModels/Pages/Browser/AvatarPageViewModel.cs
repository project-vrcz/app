using VRCZ.App.ViewModels.Entities;

namespace VRCZ.App.ViewModels.Pages.Browser;

public class AvatarPageViewModel(AvatarViewModel avatarViewModel) : PageViewModelBase
{
    public AvatarViewModel AvatarViewModel => avatarViewModel;
}
