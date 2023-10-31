using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class CheckView : Window
{
    public CheckView()
    {
        InitializeComponent();
        DataContext = new CheckViewModel();
    }
}