﻿namespace Coffee.ViewModels;

public abstract class PageViewModelBase : ViewModelBase
{
    public abstract bool OpenMenuDishesPage { get; protected set; }
    public abstract bool OpenEmployeesPage { get; protected set; }
    public abstract bool OpenPromotionPage { get; protected set; }
    public abstract bool OpenProfilePage { get; protected set; }
    public abstract bool OpenСategoriesPage { get; protected set; }
    public abstract bool OpenOrdersPage { get; protected set; }
}