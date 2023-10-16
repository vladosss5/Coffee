using System.Collections.ObjectModel;
using System.Linq;
using Coffee.Models;
using DynamicData;
using ReactiveUI;

namespace Coffee.ViewModels;

public class CheckVM : ViewModelBase
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
    
    public CheckVM()
    {
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        OrderDishes = new ObservableCollection<OrderDish>(Helper.GetContext().OrderDishes.ToList());
        Orders = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        LastOrder = Orders.OrderByDescending(x => x.IdOrder).FirstOrDefault();
        FillingDishesInCheck();
    }

    private void FillingDishesInCheck()
    {
        LastOrder = Helper.GetContext().Orders.OrderByDescending(o => o.IdOrder).FirstOrDefault();
        CheckPrice = LastOrder.FullPrice;
        var idDishes = LastOrder.OrderDishes.Select(orderDish => orderDish.IdDish).ToList();
        foreach (var id in idDishes)
        {
            var edentity = Dishes.FirstOrDefault(x => x.IdDish == id);
            if (edentity != null)
            {
                _dishesInCheck.Add(new Dish()
                {
                    IdDish =  edentity.IdDish,
                    Name = edentity.Name,
                    Price = edentity.Price,
                    Photo = edentity.Photo,
                });
            }
        }
    }
}