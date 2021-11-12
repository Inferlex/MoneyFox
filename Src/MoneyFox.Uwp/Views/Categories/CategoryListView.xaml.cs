﻿using MoneyFox.Application.Resources;
using MoneyFox.Uwp.Services;
using MoneyFox.Uwp.ViewModels.Categories;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

#nullable enable
namespace MoneyFox.Uwp.Views.Categories
{
    public sealed partial class CategoryListView
    {
        public CategoryListView()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.CategoryListVm;
        }

        public override string Header => Strings.CategoriesTitle;

        private CategoryListViewModel ViewModel => (CategoryListViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
            => ViewModel.Subscribe();

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
            => ViewModel.Unsubscribe();

        private async void AddNewCategoryClick(object sender, RoutedEventArgs e)
            => await new AddCategoryDialog {RequestedTheme = ThemeSelectorService.Theme}.ShowAsync();
    }
}