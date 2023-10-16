using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class MenuPageView : UserControl
{
    public MenuPageView()
    {
        InitializeComponent();
        DataContext = new MenuPageViewModel();
    }
}