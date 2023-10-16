using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class AuthorizationView : Window
{
    public AuthorizationView()
    {
        InitializeComponent();
        DataContext = new AuthorizationVM();
    }
}