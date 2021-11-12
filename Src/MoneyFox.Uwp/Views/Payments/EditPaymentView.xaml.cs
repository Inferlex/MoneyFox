﻿using MoneyFox.Uwp.ViewModels.Payments;
using Windows.UI.Xaml.Navigation;

#nullable enable
namespace MoneyFox.Uwp.Views.Payments
{
    public sealed partial class EditPaymentView : BaseView
    {
        public EditPaymentView()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.EditPaymentVm;
        }

        private EditPaymentViewModel ViewModel => (EditPaymentViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Subscribe();
            var vm = (PaymentViewModel)e.Parameter;
            ViewModel.InitializeCommand.Execute(vm.Id);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) => ViewModel.Unsubscribe();
    }
}