using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
    private ObservableCollection<StatusesOrder> _statusesOrders;
    private ObservableCollection<Cooking> _cookings;
    private ObservableCollection<Cooking> _logCookings = new ObservableCollection<Cooking>();
    private ObservableCollection<Order> _getOrder = new ObservableCollection<Order>();
    private ObservableCollection<Order> _setOrder = new ObservableCollection<Order>();
    
    public static User AuthUserNow { get; set; }
    
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

    public ObservableCollection<StatusesOrder> StatusesOrders
    {
        get => _statusesOrders;
        set => this.RaiseAndSetIfChanged(ref _statusesOrders, value);
    }

    private ObservableCollection<Cooking> Cookings
    {
        get => _cookings;
        set => this.RaiseAndSetIfChanged(ref _cookings, value);
    }

    private ObservableCollection<Cooking> logCookings
    {
        get => _logCookings;
        set => this.RaiseAndSetIfChanged(ref _logCookings, value);
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
        AuthUserNow = AuthorizationViewModel.AuthUser;
        AllOrders = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        OrderDishes = new ObservableCollection<OrderDish>(Helper.GetContext().OrderDishes.ToList());
        StatusesOrders = new ObservableCollection<StatusesOrder>(Helper.GetContext().StatusesOrders.ToList());
        Cookings = new ObservableCollection<Cooking>(Helper.GetContext().Cookings.ToList());
        GetOrder.AddRange(AllOrders.Where(o => o.IdStatus == 1));
        SetOrder.AddRange(AllOrders.Where(o => (o.IdStatus != 1) && o.DateAndTime.Day == DateTime.Now.Day));
    }

    public void GetOrderImpl(Order order)
    {
        order.IdStatus = 2;
        GetOrder.Remove(order);
        SetOrder.Add(order);
        db.Orders.Update(order);
        db.SaveChangesAsync();
        LoggingOrders(order);
    }

    private void LoggingOrders(Order order)
    {
        Cooking cooking = new Cooking();
        cooking.IdOrder = order.IdOrder;
        cooking.IdUser = AuthUserNow.IdUser;
        cooking.DateAdmission = DateTime.Now;

        db.Cookings.AddAsync(cooking);
        db.SaveChangesAsync();
    }

    public void SetOrderImpl(Order order)
    {
        order.IdStatus = 3;
        db.Orders.Update(order);
        db.SaveChanges();
        SetOrder.Remove(order);
        SetOrder.Add(order);
        IssueOrder(order);
    }

    public async void IssueOrder(Order order)
    {
        await Task.Delay(4000);
        order.IdStatus = 4;
        db.Orders.Update(order);
        db.SaveChanges();
        SetOrder.Remove(order);
        SetOrder.Add(order);
    }
}