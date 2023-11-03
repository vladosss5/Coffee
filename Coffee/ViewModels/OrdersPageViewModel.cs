using System;
using System.Collections.ObjectModel;
using System.Linq;
using Coffee.Models;
using ReactiveUI;

namespace Coffee.ViewModels;

public class OrdersPageViewModel : PageViewModelBase
{
    private ObservableCollection<Order> _orders;
    private ObservableCollection<Cooking> _cookings;
    private ObservableCollection<OrderDish> _orderDishes;
    private ObservableCollection<Dish> _dishes;
    private ObservableCollection<StatusesOrder> _statusesOrders;

    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }

    public ObservableCollection<Cooking> Cookings
    {
        get => _cookings;
        set => this.RaiseAndSetIfChanged(ref _cookings, value);
    }

    public ObservableCollection<OrderDish> OrderDishes
    {
        get => _orderDishes;
        set => this.RaiseAndSetIfChanged(ref _orderDishes, value);
    }

    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;
        set => this.RaiseAndSetIfChanged(ref _dishes, value);
    }
    
    public ObservableCollection<StatusesOrder> StatusesOrders
    {
        get => _statusesOrders;
        set => this.RaiseAndSetIfChanged(ref _statusesOrders, value);
    }
    
    private bool _openOrderPage;
    public override bool OpenOrdersPage
    {
        get => _openOrderPage;
        protected set => this.RaiseAndSetIfChanged(ref _openOrderPage, value);
    }
    
    public override bool OpenProfilePage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    public override bool OpenPromotionPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    public override bool OpenMenuDishesPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    public override bool OpenEmployeesPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public override bool OpenСategoriesPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    public OrdersPageViewModel()
    {
        Orders = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        Cookings = new ObservableCollection<Cooking>(Helper.GetContext().Cookings.ToList());
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        OrderDishes = new ObservableCollection<OrderDish>(Helper.GetContext().OrderDishes.ToList());
        StatusesOrders = new ObservableCollection<StatusesOrder>(Helper.GetContext().StatusesOrders.ToList());
    }
}