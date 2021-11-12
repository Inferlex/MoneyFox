﻿using MoneyFox.ViewModels.Dashboard;
using Xamarin.Forms;

namespace MoneyFox.Views.Dashboard
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.DashboardViewModel;
        }

        private DashboardViewModel ViewModel => (DashboardViewModel)BindingContext;

        protected override async void OnAppearing()
        {
            ViewModel.Subscribe();
            await ViewModel.InitializeAsync();
        }

        protected override void OnDisappearing() => ViewModel.Unsubscribe();
    }
}