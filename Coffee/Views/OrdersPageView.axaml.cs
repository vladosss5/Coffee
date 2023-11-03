using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class OrdersPageView : UserControl
{
    public OrdersPageView()
    {
        InitializeComponent();
        DataContext = new OrdersPageViewModel();
    }
}