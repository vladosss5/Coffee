using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.Models;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class EmployeesPageView : UserControl
{
    public EmployeesPageView()
    {
        InitializeComponent();
        DataContext = new EmployeesPageViewModel();
    }
    
    public void RemoveEmployee(User user)
    {
        (DataContext as EmployeesPageViewModel).RemoveEmployeeImpl(user);
    }
}