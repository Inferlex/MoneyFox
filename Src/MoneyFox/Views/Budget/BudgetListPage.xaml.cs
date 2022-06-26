﻿namespace MoneyFox.Views.Budget
{

    using ViewModels.Budget;

    public partial class BudgetListPage
    {
        public BudgetListPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<BudgetListPageViewModel>();
        }

        private BudgetListPageViewModel PageViewModel => (BudgetListPageViewModel)BindingContext;

        protected override async void OnAppearing()
        {
            await PageViewModel.InitializeCommand.ExecuteAsync(null);
        }
    }

}
