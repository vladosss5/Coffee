using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;
using Coffee.Views;

namespace Coffee;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new SellerView()
            {
                DataContext = new SellerViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}