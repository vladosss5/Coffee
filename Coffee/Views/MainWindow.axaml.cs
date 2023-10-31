using Avalonia.Controls;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}