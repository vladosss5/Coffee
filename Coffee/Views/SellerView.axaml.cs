using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Models;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class SellerView : Window
{
    public SellerView()
    {
        InitializeComponent();
        DataContext = new SellerVM();
    }
    
    public void CountPlus(Dish dish)
    {
        char flag = '+';
        (DataContext as SellerVM).EditCountDishImpl(dish, flag);
    }
    
    public void CountMinus(Dish dish)
    {
        char flag = '-';
        (DataContext as SellerVM).EditCountDishImpl(dish, flag);
    }
}