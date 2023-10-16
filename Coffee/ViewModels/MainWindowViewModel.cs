using System.Reactive;
using Avalonia.Controls;
using Coffee.Views;
using ReactiveUI;

namespace Coffee.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ReactiveCommand<Window, Unit> Authorization { get; }
    public ReactiveCommand<Window, Unit> Seller { get; }
    public ReactiveCommand<Window, Unit> TopDishes { get; }

    public MainWindowViewModel()
    {
        Authorization = ReactiveCommand.Create<Window>(OpenAuthorizationWindowImpl);
        Seller = ReactiveCommand.Create<Window>(OpenSellerWindowImpl);
        TopDishes = ReactiveCommand.Create<Window>(OpenTopDishesWindowImpl);
    }
    

    private void OpenTopDishesWindowImpl(Window obj)
    {
        GoListView glv = new GoListView();
        glv.Show();
    }

    private void OpenSellerWindowImpl(Window obj)
    {
        SellerView sv = new SellerView();
        sv.Show();
        obj.Close();
    }

    private void OpenAuthorizationWindowImpl(Window obj)
    {
        AuthorizationView av = new AuthorizationView();
        av.Show();
        obj.Close();
    }
}