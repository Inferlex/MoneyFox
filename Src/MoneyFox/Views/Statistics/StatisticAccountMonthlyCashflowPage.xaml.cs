﻿using MoneyFox.ViewModels.Statistics;
using MoneyFox.Views.Dialogs;
using System;

namespace MoneyFox.Views.Statistics
{
    public partial class StatisticAccountMonthlyCashFlowPage
    {
        public StatisticAccountMonthlyCashFlowPage()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.StatistcAccountMonthlyCashflowViewModel;
        }

        private StatisticAccountMonthlyCashflowViewModel ViewModel =>
            (StatisticAccountMonthlyCashflowViewModel)BindingContext;

        protected override void OnAppearing() => ViewModel.InitCommand.Execute(null);

        private async void OpenFilterDialog(object sender, EventArgs e)
            => await new DateSelectionPopup(ViewModel.StartDate, ViewModel.EndDate).ShowAsync();
    }
}