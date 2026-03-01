using Avalonia.Controls;
using Avalonia.Controls.Templates;
using VRCZ.App.ViewModel;
using VRCZ.App.Views;

namespace VRCZ.App;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        return param switch
        {
            GameLogViewModel vm => new GameLogView { DataContext = vm },
            _ => new TextBlock { Text = "Not Found: " + param?.GetType().FullName }
        };
    }

    public bool Match(object? data) => data is IViewModel;
}