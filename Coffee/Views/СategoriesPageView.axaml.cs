using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Models;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class СategoriesPageView : UserControl
{
    public СategoriesPageView()
    {
        InitializeComponent();
        DataContext = new СategoriesPageViewModel();
    }

    public void RemoveCategory(Category category)
    {
        (DataContext as СategoriesPageViewModel).RemoveCategoryImpl(category);
    }
}