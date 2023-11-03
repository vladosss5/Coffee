using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Coffee.Context;
using Coffee.Models;
using Metsys.Bson;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Coffee.ViewModels;

public class MenuPageViewModel : PageViewModelBase
{
    private MyDbContext db = new MyDbContext();
    
    public string ImagePath;
    public string DestImagePath;
    public string ImageProgectPath;
    public string AssetsUserPath = @"C:\Users\V-pc\Documents\Coffee\Coffee\AssetsUser";

    private string _Name;
    private float _Price;
    
    private string _selectedImagePath;
    private bool _OpenMenuDishesPage;

    private Dish _newDish = new Dish();
    
    private ObservableCollection<Dish> _dish;
    private ObservableCollection<Category> _category;
    private ObservableCollection<DishCategory> _dishCategories;
    
    public string Name
    {
        get => _Name;
        set => this.RaiseAndSetIfChanged(ref _Name, value);
    }
    
    public float Price
    {
        get => _Price;
        set => this.RaiseAndSetIfChanged(ref _Price, value);
    }
    
    
    public ObservableCollection<Dish> Dish
    {
        get => _dish;
        set => this.RaiseAndSetIfChanged(ref _dish, value);
    }
    
    public ObservableCollection<Category> Category
    {
        get => _category;
        set => this.RaiseAndSetIfChanged(ref _category, value);
    }

    public ObservableCollection<DishCategory> DishCategories
    {
        get => _dishCategories;
        set => this.RaiseAndSetIfChanged(ref _dishCategories, value);
    }
    
    public string SelectedImagePath
    {
        get => _selectedImagePath;
        set => this.RaiseAndSetIfChanged(ref _selectedImagePath, value);
    }
    
    public override bool OpenMenuDishesPage
    {
        get => _OpenMenuDishesPage;
        protected set => this.RaiseAndSetIfChanged(ref _OpenMenuDishesPage, value);
    }
    
    public override bool OpenEmployeesPage 
    { 
        get => true;
        protected set => throw new NotSupportedException(); 
    }
    
    public override bool OpenPromotionPage 
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
    
    public override bool OpenOrdersPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public ReactiveCommand<Unit, Unit> AddDish { get; }
    public ReactiveCommand<Window, Unit> SelectImge { get; }

    public MenuPageViewModel()
    {
        Dish = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        Category = new ObservableCollection<Category>(Helper.GetContext().Categories.ToList());
        DishCategories = new ObservableCollection<DishCategory>(Helper.GetContext().DishCategories.ToList());
        
        OpenMenuDishesPage = false;
        
        AddDish = ReactiveCommand.Create(AddDishImpl);
        SelectImge = ReactiveCommand.Create<Window>(SelectImgeImpl);
    }

    private async void SelectImgeImpl(Window obj)
    {
        var topLevel = TopLevel.GetTopLevel(obj);
        
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Выберите изображение",
            AllowMultiple = false,
        });

        ImagePath = Convert.ToString(files[0].Path.LocalPath);
        DestImagePath = $"{AssetsUserPath}/{files[0].Name}";
        SelectedImagePath = ImagePath;
        ImageProgectPath = $"AssetsUser/{files[0].Name}";

    }
    
    private void AddDishImpl()
    {
        var context = Helper.GetContext();
        // var categories = context.Categories.
        //     Where(x => _category.Select(c => c.SelectCategory == true))
        var dish = Helper.GetContext().Dishes.FirstOrDefault(x=> x.Name == Name);
        
        if (dish == null)
        {
            _newDish.Name = Name;
            _newDish.Price = Price;
            _newDish.Photo = ImageProgectPath;
            // _newDish.DishCategories = _category.Select(c => new DishCategory(){ IdCategory = c.IdCategory, IdDishNavigation = c}).FirstOrDefault();
            // _newDish.DishCategories = dish.
            Helper.GetContext().Dishes.Add(_newDish);
            Helper.GetContext().SaveChanges();
            try
            {
                File.Copy(ImagePath, DestImagePath, true);
                MessageBoxManager.GetMessageBoxStandard("Успех", "Блюдо добавлено", ButtonEnum.Ok, Icon.Success).ShowAsync();
            }
            catch (Exception e)
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка", $"{e}", ButtonEnum.Ok, Icon.Error).ShowAsync();
            }
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Неверно указаны двнные", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }

    public void RemoveDishImpl(Dish d)
    {
        _dish.Remove(d);
        Dish dish = db.Dishes.Where(p => p.IdDish == d.IdDish).FirstOrDefault();
        db.Dishes.Remove(dish);
        db.SaveChanges();
    }
}