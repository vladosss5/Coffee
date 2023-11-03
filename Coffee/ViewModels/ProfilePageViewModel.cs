using System;
using System.Reactive;
using Avalonia.Controls;
using Coffee.Models;
using Coffee.Views;
using Metsys.Bson;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Coffee.ViewModels;

public class ProfilePageViewModel : PageViewModelBase
{
    public static User _AuthUserNow { get; set; }
    
    private bool _OpenProfilePage;
    
    private string _firstpassword;
    private string _secondpassword;
    private string _oldpassword;
    
    public string OldPassword
    {
        get => _oldpassword;
        set => this.RaiseAndSetIfChanged(ref _oldpassword, value);
    }
    public string FirstPassword
    {
        get => _firstpassword;
        set => this.RaiseAndSetIfChanged(ref _firstpassword, value);
    }
    public string SecondPassword
    {
        get => _secondpassword;
        set => this.RaiseAndSetIfChanged(ref _secondpassword, value);
    }

    public override bool OpenProfilePage
    {
        get => _OpenProfilePage;
        protected set => this.RaiseAndSetIfChanged(ref _OpenProfilePage, value);
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
    
    public override bool OpenOrdersPage
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    
    public ReactiveCommand<Window, Unit> ChangePassword { get; }
    
    public ProfilePageViewModel()
    {
        _AuthUserNow = AdminMainViewModel.AuthUserNow;
        ChangePassword = ReactiveCommand.Create<Window>(ChangePasswordImpl);
    }
    
    private void ChangePasswordImpl(Window obj)
    {
        if (_oldpassword == AuthorizationViewModel.AuthUser.Password)
        {
            if (_oldpassword != _firstpassword)
            {
                if (_firstpassword == _secondpassword)
                {
                    AuthorizationView av = new AuthorizationView();
                    AuthorizationViewModel.AuthUser.Password = _firstpassword;
                    Helper.GetContext().Users.Update(AuthorizationViewModel.AuthUser);
                    Helper.GetContext().SaveChanges();
                    MessageBoxManager.GetMessageBoxStandard("Успех", "Пароль изменён", ButtonEnum.Ok, Icon.Success).ShowWindowDialogAsync(obj);
                    av.Show();
                    // System.Threading.Thread.Sleep(5000);
                    obj.Close();
                }
                else
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка","Пароли не совпадают",  ButtonEnum.Ok, Icon.Error).ShowWindowDialogAsync(obj);
                    return;
                }
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка","Нельзя использовать старый пароль",  ButtonEnum.Ok, Icon.Error).ShowWindowDialogAsync(obj);
                return;
            }
            
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Ошибка","Неверный текущий пароль",  ButtonEnum.Ok, Icon.Error).ShowWindowDialogAsync(obj);
            return;
        }
    }
}