﻿using System;

#nullable enable
namespace MoneyFox.Uwp.Services
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        void Initialize(object frame);

        bool Navigate<TViewModel>(object? parameter = null);

        bool Navigate(Type viewModelType, object? parameter = null);

        bool GoBack();

        bool GoForward();
    }
}