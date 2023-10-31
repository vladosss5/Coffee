using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Coffee.Models;
using Coffee.Views;
using Metsys.Bson;
using ReactiveUI;

namespace Coffee.ViewModels;

public class CheckViewModel : ViewModelBase
{
    private ObservableCollection<Dish> _dishes;
    private ObservableCollection<OrderDish> _orderDishes;
    private ObservableCollection<Order> _orders;
    private ObservableCollection<Dish> _dishesInCheck = new ObservableCollection<Dish>();
    private Order _lastOrder = new Order();
    private float _checkPrice;

    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;
        set => this.RaiseAndSetIfChanged(ref _dishes, value);
    }

    public ObservableCollection<OrderDish> OrderDishes
    {
        get => _orderDishes;
        set => this.RaiseAndSetIfChanged(ref _orderDishes, value);
    }

    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }

    public Order LastOrder
    {
        get => _lastOrder;
        set => this.RaiseAndSetIfChanged(ref _lastOrder, value);
    }

    public ObservableCollection<Dish> DishesInCheck
    {
        get => _dishesInCheck;
        set => this.RaiseAndSetIfChanged(ref _dishesInCheck, value);
    }

    public float CheckPrice
    {
        get => _checkPrice;
        set => this.RaiseAndSetIfChanged(ref _checkPrice, value);
    }

    public ReactiveCommand<Window, Unit> OpenSellerWindow { get; }

    public CheckViewModel()
    {
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        OrderDishes = new ObservableCollection<OrderDish>(Helper.GetContext().OrderDishes.ToList());
        Orders = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        OpenSellerWindow = ReactiveCommand.Create<Window>(OpenSellerWindowImpl);
        FillingDishesInCheck();
    }

    private void OpenSellerWindowImpl(Window obj)
    {
        SellerView sv = new SellerView();
        sv.Show();
        obj.Close();
    }


    private void FillingDishesInCheck()
    {
        LastOrder = Helper.GetContext().Orders.OrderByDescending(o => o.IdOrder).FirstOrDefault();
        CheckPrice = LastOrder.FullPrice;
        var idDishes = LastOrder.OrderDishes.Select(orderDish => orderDish.IdDish).ToList();
        
        foreach (var id in idDishes)
        {
            var edentity = Dishes.FirstOrDefault(x => x.IdDish == id);
            edentity.CountDishes = OrderDishes.
                Where(x => x.IdDish == id && x.IdOrder == _lastOrder.IdOrder).
                FirstOrDefault().CountDishes;
            if (edentity != null)
            {
                _dishesInCheck.Add(new Dish()
                {
                    IdDish =  edentity.IdDish,
                    Name = edentity.Name,
                    Price = edentity.Price * edentity.CountDishes,
                    Photo = edentity.Photo, 
                    CountDishes = edentity.CountDishes
                });
            }
        }
    }
}