using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Coffee.Context;
using Coffee.Models;
using Metsys.Bson;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Coffee.ViewModels;

public class PromotionPageViewModel : PageViewModelBase
{
    private Promotion _newPromotion = new Promotion();
    private ObservableCollection<Promotion> Promotions = new ObservableCollection<Promotion>();
    private ObservableCollection<Dish> _dish;
    private ObservableCollection<Promotion> _promotion;
    private ObservableCollection<Dish> _dishList = new ObservableCollection<Dish>();
    private DateTimeOffset _dateEndAction = DateTimeOffset.Now;
    private int _discountPercent = 0;
    private MyDbContext db = new MyDbContext();

    public int DiscountPercent
    {
        get => _discountPercent;
        set => this.RaiseAndSetIfChanged(ref _discountPercent, value);
    }
    
    public DateTimeOffset DateEndAction
    {
        get => _dateEndAction;
        set => this.RaiseAndSetIfChanged(ref _dateEndAction, value);
    }
    
    
    private bool _OpenPromotionPage;
    public override bool OpenPromotionPage
    {
        get => _OpenPromotionPage;
        protected set => this.RaiseAndSetIfChanged(ref _OpenPromotionPage, value);
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

    public override bool OpenProfilePage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public override bool OpenСategoriesPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public ObservableCollection<Promotion> Promotion
    {
        get => _promotion;
        set => this.RaiseAndSetIfChanged(ref _promotion, value);
    }

    public ObservableCollection<Dish> Dishes
    {
        get => _dish;
        set => this.RaiseAndSetIfChanged(ref _dish, value);
    }

    public ObservableCollection<Dish> DishesLists
    {
        get => _dishList;
        set => this.RaiseAndSetIfChanged(ref _dishList, value);
    }
    
    public ReactiveCommand<Unit, Unit> AddPromotion { get; }

    public PromotionPageViewModel()
    {
        OpenPromotionPage = false;
        Promotion = new ObservableCollection<Promotion>(Helper.GetContext().Promotions.ToList());
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        FillingStatus();
        AddPromotion = ReactiveCommand.Create(AddPromotionImpl);
    }

    private void FillingStatus()
    {
        for (int i = 0; i < Promotion.Count; ++i)
        {
            if (Promotion[i].FinishDate < DateTime.Now)
            {
                Promotion[i].Activity = "Не активно";
            }
            else
            {
                Promotion[i].Activity = "Активно";
            }
        }
    }

    private void AddPromotionImpl()
    {
        var context = Helper.GetContext();
        foreach (var d in DishesLists)
        {
            _newPromotion.IdDish = d.IdDish;
            _newPromotion.FinishDate = _dateEndAction.DateTime;
            _newPromotion.DiscountPercent = _discountPercent;
            _newPromotion.TotalPrice = d.Price - (d.Price * _discountPercent * 0.01);
            context.Promotions.Add(_newPromotion);
            context.SaveChanges();
            _newPromotion = new Promotion();
        }
        
        DishesLists.Clear();
        MessageBoxManager.GetMessageBoxStandard("Успех", "Акция добавлена", ButtonEnum.Ok).ShowAsync();
    }

    public void AddDishPrePromotionImpl(Dish dish)
    {
        var edentity = _dishList.SingleOrDefault(x => x.Name == dish.Name);

        if (edentity == null)
        {
            _dishList.Add(new Dish
            {
                IdDish = dish.IdDish,
                Name = dish.Name,
                Price = dish.Price * dish.CountDishes,
                CountDishes = dish.CountDishes,
                Photo = dish.Photo
            });
        }
        else
        {
            _dishList.Remove(edentity);
            var p0 = new Dish
            {
                Name = dish.Name,
                Price = dish.Price * dish.CountDishes,
                CountDishes = dish.CountDishes,
                Photo = dish.Photo
            };
            p0.IdDish = dish.IdDish;
            _dishList.Add(p0);
        }
        var date = DateEndAction;
    }

    public void RemoveDishPrePromotionImpl(Dish dish)
    {
        _dishList.Remove(dish);
    }

    public void RemovePromotionImpl(Promotion prom)
    {
        _promotion.Remove(prom);
        Promotion promotion = db.Promotions.Where(p => p.IdPromotion == prom.IdPromotion).FirstOrDefault();
        db.Promotions.Remove(promotion);
        db.SaveChanges();
    }
}