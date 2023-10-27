using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Context;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // MyDbContext MyDbContext = new MyDbContext();
        // MyDbContext.Database.EnsureCreated();
        DataContext = new MainWindowViewModel();
    }
}