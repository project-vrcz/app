using System.Collections.Specialized;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using VRCZ.App.ViewModel;

namespace VRCZ.App.Views;

public partial class GameLogView : UserControl
{
    private GameLogViewModel ViewModel => DataContext as GameLogViewModel ??
                                          throw new InvalidOperationException(
                                              "DataContext must be of type GameLogViewModel");

    public GameLogView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        ViewModel.LogEntities.CollectionChanged += OnLogEntitiesCollectionChanged;
        ViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        ViewModel.LogEntities.CollectionChanged -= OnLogEntitiesCollectionChanged;
        ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(GameLogViewModel.ScrollToEnd) && ViewModel.ScrollToEnd)
        {
            Dispatcher.UIThread.Post(() => LogEntitiesListBox.ScrollIntoView(ViewModel.LogEntities.Count - 1));
        }
    }

    private void OnLogEntitiesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (!ViewModel.ScrollToEnd)
            return;

        Dispatcher.UIThread.Post(() => LogEntitiesListBox.ScrollIntoView(ViewModel.LogEntities.Count - 1));
    }
}