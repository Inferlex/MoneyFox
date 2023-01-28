﻿namespace MoneyFox.Core.Tests.Common.Helper;

using Core.Common.Helpers;
using FluentAssertions;
using MoneyFox.Domain.Aggregates.AccountAggregate;

public class RecurringPaymentHelperTests
{
    [Theory]
    [InlineData(PaymentRecurrence.Daily, false)]
    [InlineData(PaymentRecurrence.Weekly, false)]
    [InlineData(PaymentRecurrence.Biweekly, false)]
    [InlineData(PaymentRecurrence.Monthly, true)]
    [InlineData(PaymentRecurrence.Bimonthly, true)]
    [InlineData(PaymentRecurrence.Quarterly, true)]
    [InlineData(PaymentRecurrence.Biannually, true)]
    [InlineData(PaymentRecurrence.Yearly, true)]
    public void CheckAllowLastDayOfMonth(PaymentRecurrence recurrence, bool expectedAllowLastDayOfMonth)
    {
        RecurringPaymentHelper.AllowLastDayOfMonth(recurrence).Should().Be(expectedAllowLastDayOfMonth);
    }
}
