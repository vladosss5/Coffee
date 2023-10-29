using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Coffee.Models;
using Coffee.Views;
using DynamicData;
using ReactiveUI;

namespace Coffee.ViewModels;

public class SellerVM : ViewModelBase
{
    private ObservableCollection<Order> _order;
    private ObservableCollection<Dish> _dishes;
    private ObservableCollection<DishCategory> _dishCategories;
    private ObservableCollection<Category> _categories;
    private ObservableCollection<Dish> _dishesInSelectCat = new ObservableCollection<Dish>();
    private ObservableCollection<Dish> _dishesInCart = new ObservableCollection<Dish>();
    private static Category _selectCategory;
    private static Dish _selectedDishes;
    private static float _prePrice;
    public Order LastOrder;
    
    public ObservableCollection<Order> Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }
    public ObservableCollection<Dish> DishesInSelectCat
    {
        get => _dishesInSelectCat;
        set
        {
            this.RaiseAndSetIfChanged(ref _dishesInSelectCat, value);
            
        }
    }

    public Category SelectCategory
    {
        get => _selectCategory;
        set
        {
            DishesInSelectCat.Clear();
            this.RaiseAndSetIfChanged(ref _selectCategory, value);

            var dishes = _dishes
                .SelectMany(d => d.DishCategories)
                .Where(x => x.IdCategory == _selectCategory.IdCategory)
                .Select(x => x.IdDishNavigation)
                .ToList();
            DishesInSelectCat.AddRange(dishes);
        }
    }
    
    public Dish SelectedDishes
    {
        get => _selectedDishes;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedDishes, value);

            if (SelectedDishes != null)
            {
                var edentity = _dishesInCart.SingleOrDefault(x => x.Name == SelectedDishes.Name);
                
                if (edentity == null)
                {
                    DishesInCart.Add(SelectedDishes);
                }
                else
                {
                    DishesInCart.Remove(edentity);
                    DishesInCart.Add(SelectedDishes);
                }
            }

            PrePrice = _dishesInCart.Sum(x => x.Price * x.CountDishes);
        }
     
    }

    public ObservableCollection<Dish> DishesInCart
    {
        get => _dishesInCart;
        set => this.RaiseAndSetIfChanged(ref _dishesInCart, value);
    }

    
    public ObservableCollection<Dish> Dish
    {
        get => _dishes;
        set => this.RaiseAndSetIfChanged(ref _dishes, value);
    }

    public ObservableCollection<DishCategory> DishCategory
    {
        get => _dishCategories;
        set => this.RaiseAndSetIfChanged(ref _dishCategories, value);
    }
    
    public ObservableCollection<Category> Category
    {
        get => _categories;
        set => this.RaiseAndSetIfChanged(ref _categories, value);
    }

    public float PrePrice
    {
        get => _prePrice;
        set => this.RaiseAndSetIfChanged(ref _prePrice, value);
    }
    
    public ReactiveCommand<Window, Unit> BuyButton { get; }
    
    public SellerVM()
    {
        DishCategory = new ObservableCollection<DishCategory>(Helper.GetContext().DishCategories.ToList());
        Category = new ObservableCollection<Category>(Helper.GetContext().Categories.ToList());
        Dish = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        BuyButton = ReactiveCommand.Create<Window>(CreateOrderImpl);
    }

    private void CreateOrderImpl(Window obj)
    {
        var context = Helper.GetContext();
        var dishes = context.Dishes.
            Where(x => _dishesInCart.Select(x => x.IdDish).
                Contains(x.IdDish)).ToList();

        Order order = new Order();
        order.DateAndTime = DateTime.Now;
        order.FullPrice = _dishesInCart.Sum(x => x.Price * x.CountDishes);
        order.OrderDishes = dishes.Select(x => new OrderDish() { IdDishNavigation = x, CountDishes = x.CountDishes }).ToList();
        order.IdStatus = 1;
        Helper.GetContext().Orders.Add(order);
        Helper.GetContext().Orders.UpdateRange();
        Helper.GetContext().SaveChanges();

        // foreach (var d in DishesInSelectCat)
        // {
        //     d.CountDishes = 1;
        // }
        
        CheckView cvw = new CheckView();
        cvw.Show();
        obj.Close();
    }
    
    public void EditCountDishImpl(Dish dish, char f)
    {
        int i = 0;
        
        if (f == '+')
        {
            dish.CountDishes += 1;
        }
        else
        {
            if (dish.CountDishes > 0)
            {
                dish.CountDishes -= 1;
            }
        }

        foreach (var d in DishesInSelectCat)
        {
            if (d == dish)
            {
                break;
            }
            else
            {
                i++;
            }
        }
        
        DishesInSelectCat[i] = dish;
    }
}