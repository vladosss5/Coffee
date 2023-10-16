using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Coffee.Models;
using ReactiveUI;

namespace Coffee.ViewModels;

public class MenuPageViewModel : PageViewModelBase
{
    private string _PathImage;
    public string PathImage
    {
        get => PathImage;
        set => this.RaiseAndSetIfChanged(ref _PathImage, value);
    }

    private string _Name;
    public string Name
    {
        get => _Name;
        set => this.RaiseAndSetIfChanged(ref _Name, value);
    }

    private float _Price;
    public float Price
    {
        get => _Price;
        set => this.RaiseAndSetIfChanged(ref _Price, value);
    }
    
    private ObservableCollection<Dish> _dish;
    public ObservableCollection<Dish> Dish
    {
        get => _dish;
        set => this.RaiseAndSetIfChanged(ref _dish, value);
    }

    private ObservableCollection<Category> _category;
    public ObservableCollection<Category> Category
    {
        get => _category;
        set => this.RaiseAndSetIfChanged(ref _category, value);
    }
    
    private string _selectedImagePath;
    public string SelectedImagePath
    {
        get => _selectedImagePath;
        set => this.RaiseAndSetIfChanged(ref _selectedImagePath, value);
    }

    private bool _OpenMenuDishesPage;
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
    
    public ICommand AddDish { get; }
    public ReactiveCommand<Window, Unit> SelectImge { get; }

    public MenuPageViewModel()
    {
        Dish = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        Category = new ObservableCollection<Category>(Helper.GetContext().Categories.ToList());
        OpenMenuDishesPage = false;
        AddDish = ReactiveCommand.Create(AddDishImpl);
        SelectImge = ReactiveCommand.Create<Window>(SelectImgeImpl);
    }

    Task<IReadOnlyList<IStorageFile>> OpenFilePickerAsync(FilePickerOpenOptions options)
    {
        throw new NotImplementedException();
    }

    private void SelectImgeImpl(Window obj)
    {
        var storage = obj.StorageProvider;
    }
    
    private void AddDishImpl()
    {
        
    }
}