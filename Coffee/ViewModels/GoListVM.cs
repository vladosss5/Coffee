using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Coffee.Models;
using ReactiveUI;

namespace Coffee.ViewModels;

public class GoListVM : ViewModelBase
{
    private ObservableCollection<Order> _pendingOrders = new ObservableCollection<Order>();
    private ObservableCollection<Order> _readyOrders = new ObservableCollection<Order>();
    private ObservableCollection<Promotion> _promotion;
    private ObservableCollection<Order> _orders;

    public ObservableCollection<Order> PendingOrder
    {
        get => _pendingOrders;
        set => this.RaiseAndSetIfChanged(ref _pendingOrders, value);
    }
    
    public ObservableCollection<Order> ReadyOrder
    {
        get => _readyOrders;
        set => this.RaiseAndSetIfChanged(ref _readyOrders, value);
    }

    public ObservableCollection<Promotion> Promotion
    {
        get => _promotion;
        set => this.RaiseAndSetIfChanged(ref _promotion, value);
    }

    public ObservableCollection<Order> Order
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }

    public GoListVM()
    {
        Promotion = new ObservableCollection<Promotion>(Helper.GetContext().Promotions.ToList());
        Order = new ObservableCollection<Order>(Helper.GetContext().Orders.ToList());
        FillingPendingOrder();
        FillingReadyOrder();
    }

    private void FillingPendingOrder()
    {
        foreach (var order in Order)
        {
            if (order.IdStstus == 3)
            {
                _readyOrders.Add( new Order()
                {
                    IdOrder = order.IdOrder,
                    FullPrice = order.FullPrice,
                    DateAndTime = order.DateAndTime,
                    IdStstus = order.IdStstus
                });
            }
        }
    }
    
    private void FillingReadyOrder()
    {
        foreach (var order in Order)
        {
            if (order.IdStstus == 1 || order.IdStstus == 2)
            {
                _pendingOrders.Add( new Order()
                {
                    IdOrder = order.IdOrder,
                    FullPrice = order.FullPrice,
                    DateAndTime = order.DateAndTime,
                    IdStstus = order.IdStstus
                });
            }
        }
    }
}