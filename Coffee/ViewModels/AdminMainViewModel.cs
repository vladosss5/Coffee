using System;
using System.IO;
using System.Reactive;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Coffee.Models;
using Coffee.Views;
using ReactiveUI;
using Microsoft.EntityFrameworkCore;
using Excel = Microsoft.Office.Interop.Excel;

namespace Coffee.ViewModels;

public class AdminMainViewModel : ViewModelBase
{
    public static User AuthUserNow { get; set; }
    
    private ObservableCollection<Order> _order;
    public ObservableCollection<Order> Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }
    
    private PageViewModelBase _CurrentPage;
    public PageViewModelBase CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }
    
    public ReactiveCommand<Window, Unit> ExitProfile { get; }
    public ReactiveCommand<Window, Unit> CreateReport { get; }
    public ICommand OpenMenuDishPage { get; }
    public ICommand OpenEmployeesPage { get; }
    public ICommand OpenPromotionPage { get; }
    public ICommand OpenProfilePage { get; }
    
    private readonly PageViewModelBase[] Pages = 
    { 
        new MenuPageViewModel(),
        new EmployeesPageViewModel(),
        new PromotionPageViewModel(),
        new ProfilePageViewModel()
    };
    
    public AdminMainViewModel()
    {
        AuthUserNow = AuthorizationVM.AuthUser;
        ExitProfile = ReactiveCommand.Create<Window>(ExitProfileImpl);
        CreateReport = ReactiveCommand.Create<Window>(CreateReportImpl);
        
        Order = new ObservableCollection<Order>(Helper.GetContext().Orders.Include(x => x.OrderDishes).ToList());
        
        _CurrentPage = Pages[0];
        
        var canOpenMenuDish = this.WhenAnyValue(x => x.CurrentPage.OpenMenuDishesPage);
        OpenMenuDishPage = ReactiveCommand.Create(OpenMenuDishPageImpl, canOpenMenuDish);
        
        var canOpenEmployeesPage = this.WhenAnyValue(x => x.CurrentPage.OpenEmployeesPage);
        OpenEmployeesPage = ReactiveCommand.Create(OpenEmployeespageImpl, canOpenEmployeesPage);
        
        var canOpenPromotionPage = this.WhenAnyValue(x => x.CurrentPage.OpenPromotionPage);
        OpenPromotionPage = ReactiveCommand.Create(OpenPromotionPageImpl, canOpenPromotionPage);
        
        var canOpenProfilePage = this.WhenAnyValue(x => x.CurrentPage.OpenProfilePage);
        OpenProfilePage = ReactiveCommand.Create(OpenProfilePageImpl, canOpenProfilePage);
    }

    private void OpenMenuDishPageImpl()
    {
        CurrentPage = Pages[0];
    }
    
    private void OpenEmployeespageImpl()
    {
        CurrentPage = Pages[1];
    }
    
    private void OpenPromotionPageImpl()
    {
        CurrentPage = Pages[2];
    }
    
    private void OpenProfilePageImpl()
    {
        CurrentPage = Pages[3];
    }
    
    private void ExitProfileImpl(Window obj)
    {
        AuthUserNow = null;
        MainWindow mw = new MainWindow();
        mw.Show();
        obj.Close();
    }

    private void CreateReportImpl(Window obj)
    {
         using (ExcelHelper helper = new ExcelHelper())
        {
            if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory, @"D:\Документы\Отчёт о заказах.xlsx")))
            {
                int i = 0;
                var allOrders = Order.ToList().OrderBy(p => p.DateAndTime).ToList();
                var application = new Excel.Application();
                string[] month = new string[12] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", 
                                                            "Август", "Сентябрь", "Окрябрь", "Ноябрь", "Декабрь" };
                
                application.SheetsInNewWorkbook = month.Length;

                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                
                for (int j = 0; j < month.Length; ++j)
                {
                    int counter = 0;
                    int startRowIndex = 1;
                    
                    Excel.Worksheet worksheet = application.Worksheets.Item[j + 1];
                    worksheet.Name = month[j];

                    worksheet.Cells[1][startRowIndex] = "Номер";
                    worksheet.Cells[2][startRowIndex] = "Кассир";
                    worksheet.Cells[3][startRowIndex] = "Дата";
                    worksheet.Cells[4][startRowIndex] = "Цена";

                    startRowIndex++;

                    while (allOrders.Count > i)
                    {
                        if (allOrders[i].DateAndTime.Month == j + 1)
                        {
                            worksheet.Cells[1][startRowIndex] = allOrders[i].IdOrder;
                            worksheet.Cells[2][startRowIndex] = allOrders[i].DateAndTime.ToString("yy-MM-dd");
                            worksheet.Cells[3][startRowIndex] = allOrders[i].FullPrice;
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                        i++;
                        startRowIndex++;
                    }
                    
                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex],
                        worksheet.Cells[3][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "Итого:";

                    worksheet.Cells[4][startRowIndex].Formula =
                        $"=SUM(D{startRowIndex - counter}:" + $"D{startRowIndex - 1}";

                    worksheet.Columns.AutoFit();
                    helper.Save();
                }
                application.Visible = true;
            }
        }
    }
}