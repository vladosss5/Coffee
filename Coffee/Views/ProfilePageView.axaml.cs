using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class ProfilePageView : UserControl
{
    public ProfilePageView()
    {
        InitializeComponent();
        DataContext = new ProfilePageViewModel();
    }
}