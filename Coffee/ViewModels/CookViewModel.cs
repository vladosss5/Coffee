using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Coffee.Context;
using Coffee.Models;
using DynamicData;
using ReactiveUI;

namespace Coffee.ViewModels;

public class CookViewModel : ViewModelBase
{
    private ObservableCollection<Order> _allOrders;
    private ObservableCollection<Dish> _dishes;
    private ObservableCollection<OrderDish> _orderDishes;
    private ObservableCollection<Order> _getOrder = new ObservableCollection<Order>();
    private ObservableCollection<Order> _setOrder = new ObservableCollection<Order>();
    
    private MyDbContext db = new MyDbContext();

    public ObservableCollection<Order> AllOrders
    {
        get => _allOrders;
        set => this.RaiseAndSetIfChanged(ref _allOrders, value);
    }

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

    public ObservableCollection<Order> GetOrder
    {
        get => _getOrder;
        set => this.RaiseAndSetIfChanged(ref _getOrder, value);
    }

    public ObservableCollection<Order> SetOrder
    {
        get => _setOrder;
        set => this.RaiseAndSetIfChanged(ref _setOrder, value);
    }

    public CookViewModel()
    {
        AllOrders = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        OrderDishes = new ObservableCollection<OrderDish>(Helper.GetContext().OrderDishes.ToList());
        GetOrder.AddRange(AllOrders.Where(o => o.IdStatus == 1));
        SetOrder.AddRange(AllOrders.Where(o => o.IdStatus == 2 || o.IdStatus == 3));
    }

    public void GetOrderImpl(Order o)
    {
        o.IdStatus = 2;
        db.Orders.Update(o);
        db.SaveChanges();
    }
}