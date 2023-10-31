using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class GoListView : Window
{
    public GoListView()
    {
        InitializeComponent();
        DataContext = new GoListViewModel();
    }
}