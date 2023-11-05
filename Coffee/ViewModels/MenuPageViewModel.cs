using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reflection.Metadata.Ecma335;
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
    public string AssetsUserPath = @"C:\Users\V-pc\Documents\yчёба\RiderProjects\Coffee\Coffee\AssetsUser";

    private string _Name;
    private float _Price;
    
    private string _selectedImagePath;
    private bool _OpenMenuDishesPage;

    private Dish _newDish = new Dish();
    
    private ObservableCollection<Dish> _dishes;
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
    
    
    public ObservableCollection<Dish> Dishes
    {
        get => _dishes;
        set => this.RaiseAndSetIfChanged(ref _dishes, value);
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
        Dishes = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
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

        var dish = Helper.GetContext().Dishes.FirstOrDefault(x=> x.Name == Name);
        var selectCategory = _category.Where(c => c.SelectCategory == true).FirstOrDefault();
        var categories = context.Categories.
            Where(c => c.IdCategory == selectCategory.IdCategory).ToList();
        
        if (dish == null)
        {
            _newDish.Name = Name;
            _newDish.Price = Price;
            _newDish.Photo = ImageProgectPath;
            _newDish.DishCategories = categories.Select(x => new DishCategory()
                { IdCategoryNavigation = x }).ToList();
            try
            {
                File.Copy(ImagePath, DestImagePath, true);
                Helper.GetContext().Dishes.Add(_newDish);
                Helper.GetContext().SaveChanges();
                Dishes.Add(_newDish);
                MessageBoxManager.GetMessageBoxStandard("Успех", "Блюдо добавлено", ButtonEnum.Ok, Icon.Success).ShowAsync();
            }
            catch (Exception e)
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка", $"{e}", ButtonEnum.Ok, Icon.Error).ShowAsync();
            }
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Неверно указаны данные", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }

    public void RemoveDishImpl(DishCategory dc)
    {
        var dish = Dishes.Where(d => d.IdDish == dc.IdDish).FirstOrDefault();

        _dishCategories.Remove(dc);
        _dishes.Remove(dish);
        db.DishCategories.Remove(dc);
        db.Dishes.Remove(dish);
        db.SaveChanges();
    }
}