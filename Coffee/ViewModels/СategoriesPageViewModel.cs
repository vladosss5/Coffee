using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Coffee.Context;
using Coffee.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Coffee.ViewModels;

public class СategoriesPageViewModel : PageViewModelBase
{
    private ObservableCollection<Category> _categories;
    private Category _newCategory = new Category();

    private MyDbContext db = new MyDbContext();
    
    public string ImagePath;
    public string DestImagePath;
    public string ImageProgectPath;
    
    public string AssetsUserPath = @"C:\Users\V-pc\Documents\yчёба\RiderProjects\Coffee\Coffee\AssetsUser";
    
    private string _Name;
    
    private string _selectedImagePath;
    
    public string Name
    {
        get => _Name;
        set => this.RaiseAndSetIfChanged(ref _Name, value);
    }

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set => this.RaiseAndSetIfChanged(ref _categories, value);
    }
    
    public string SelectedImagePath
    {
        get => _selectedImagePath;
        set => this.RaiseAndSetIfChanged(ref _selectedImagePath, value);
    }
    
    private bool _OpenСategoriesPage;
    public override bool OpenСategoriesPage 
    { 
        get => _OpenСategoriesPage;
        protected set => this.RaiseAndSetIfChanged(ref _OpenСategoriesPage, value);
    }
    
    public override bool OpenMenuDishesPage 
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
    
    public override bool OpenEmployeesPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public ReactiveCommand<Unit, Unit> AddCategory { get; }
    public ReactiveCommand<Window, Unit> SelectImge { get; }
    
    public СategoriesPageViewModel()
    {
        Categories = new ObservableCollection<Category>(Helper.GetContext().Categories.ToList());
        AddCategory = ReactiveCommand.Create(AddCategoryImpl);
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

    private void AddCategoryImpl()
    {
        var context = Helper.GetContext();
        // var categories = context.Categories.
        //     Where(x => _category.Select(c => c.SelectCategory == true))
        var category = Helper.GetContext().Categories.FirstOrDefault(x=> x.Name == Name);
        
        if (category == null)
        {
            _newCategory.Name = Name;
            _newCategory.Photo = ImageProgectPath;
            try
            {
                File.Copy(ImagePath, DestImagePath, true);
                Categories.Add(_newCategory);
                Helper.GetContext().Categories.Add(_newCategory);
                Helper.GetContext().SaveChanges();
                MessageBoxManager.GetMessageBoxStandard("Успех", "Категория добавлена", ButtonEnum.Ok, Icon.Success).ShowAsync();
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

    public void RemoveCategoryImpl(Category category)
    {
        _categories.Remove(category);
        db.Categories.Remove(category);
        db.SaveChanges();
    }
}