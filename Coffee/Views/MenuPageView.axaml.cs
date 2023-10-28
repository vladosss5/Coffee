using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Models;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class MenuPageView : UserControl
{
    public MenuPageView()
    {
        InitializeComponent();
        DataContext = new MenuPageViewModel();
    }

    public void RemoveDish(Dish dish)
    {
        (DataContext as MenuPageViewModel).RemoveDishImpl(dish);
    }
}