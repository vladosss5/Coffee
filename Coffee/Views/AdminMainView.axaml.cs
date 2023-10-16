﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Coffee.ViewModels;

namespace Coffee.Views;

public partial class AdminMainView : Window
{
    public AdminMainView()
    {
        InitializeComponent();
        DataContext = new AdminMainViewModel();
    }
}