using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Models;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class CookView : Window
{
    public CookView()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
    }

    public void GetOrder(Order order)
    {
        (DataContext as CookViewModel).GetOrderImpl(order);
    }
    
    public void SetOrder(Order order)
    {
        (DataContext as CookViewModel).SetOrderImpl(order);
    }
}