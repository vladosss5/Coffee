using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Coffee.Context;
using Coffee.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Coffee.ViewModels;

public class EmployeesPageViewModel : PageViewModelBase
{
    private User _newUser = new User();
    private string _login;
    private string _password;
    private string _fName;
    private string _sName;
    private string _lName;
    private int _idPost;
    
    private MyDbContext db = new MyDbContext();
    
    private ObservableCollection<User> _user;
    private ObservableCollection<Post> _posts;
    public ObservableCollection<User> User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    public ObservableCollection<Post> Posts
    {
        get => _posts;
        set => this.RaiseAndSetIfChanged(ref _posts, value);
    }

    public string Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public string FName
    {
        get => _fName;
        set => this.RaiseAndSetIfChanged(ref _fName, value);
    }
    
    public string SName
    {
        get => _sName;
        set => this.RaiseAndSetIfChanged(ref _sName, value);
    }
    
    public string LName
    {
        get => _lName;
        set => this.RaiseAndSetIfChanged(ref _lName, value);
    }

    public int IdPost
    {
        get => _idPost;
        set => this.RaiseAndSetIfChanged(ref _idPost, value);
    }
    
    private bool _OpenEmployeesPage;
    public override bool OpenEmployeesPage 
    { 
        get => _OpenEmployeesPage;
        protected set => this.RaiseAndSetIfChanged(ref _OpenEmployeesPage, value);
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

    public ReactiveCommand<Unit, Unit> AddEmployee { get; }

    public EmployeesPageViewModel()
    {
        User = new ObservableCollection<User>(Helper.GetContext().Users.ToList());
        Posts = new ObservableCollection<Post>(Helper.GetContext().Posts.ToList());
        AddEmployee = ReactiveCommand.Create(AddEmployeeImpl);
        OpenEmployeesPage = false;
    }

    private void AddEmployeeImpl()
    {
        var user = Helper.GetContext().Users.FirstOrDefault(x=> x.Login == Login);
        var truePost = _posts.Where(p => p.SelectPost == true).FirstOrDefault();
        
        if (user == null)
        {
            _newUser.Login = _login;
            _newUser.Password = _password;
            _newUser.Fname = _fName;
            _newUser.Sname = _sName;
            _newUser.Lname = _lName;
            _newUser.IdPost = truePost.IdPost;
            Helper.GetContext().Users.Add(_newUser);
            Helper.GetContext().SaveChanges();
            MessageBoxManager.GetMessageBoxStandard("Успех", "Акция добавлена", ButtonEnum.Ok, Icon.Success).ShowAsync();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ощибка", "Неверно указаны двнные", ButtonEnum.Ok, Icon.Error).ShowAsync();
        }
    }

    public void RemoveEmployeeImpl(User u)
    {
        _user.Remove(u);
        User user = db.Users.Where(p => p.IdUser == u.IdUser).FirstOrDefault();
        db.Users.Remove(user);
        db.SaveChanges();
    }
}