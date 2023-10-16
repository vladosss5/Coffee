using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Models;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class PromotionPageView : UserControl
{
    public PromotionPageView()
    {
        InitializeComponent();
        DataContext = new PromotionPageViewModel();
    }
    
    public void AddDishPrePromotion(Dish dish)
    {
        (DataContext as PromotionPageViewModel).AddDishPrePromotionImpl(dish);
    }

    public void RemoveDishPrePromotion(Dish dish)
    {
        (DataContext as PromotionPageViewModel).RemoveDishPrePromotionImpl(dish);
    }

    public void RemovePromotion(Promotion promotion)
    {
        (DataContext as PromotionPageViewModel).RemovePromotionImpl(promotion);
    }
}