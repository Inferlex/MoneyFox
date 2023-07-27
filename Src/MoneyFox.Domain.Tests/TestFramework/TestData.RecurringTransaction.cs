﻿namespace MoneyFox.Domain.Tests.TestFramework;

using Domain.Aggregates;
using Domain.Aggregates.AccountAggregate;
using Domain.Aggregates.RecurringTransactionAggregate;

internal static partial class TestData
{
    public sealed class RecurringExpense : IRecurringTransaction
    {
        public RecurringTransactionId Id => new(100);
        public DateOnly StartDate { get; } = new(year: 2023, month: 07, day: 12);
        public DateOnly? EndDate { get; } = null;
        public Money Amount { get; } = new(amount: 1042, currency: Currencies.CHF);
        public PaymentType Type => PaymentType.Expense;
        public string? Note { get; } = null;
        public int ChargedAccount { get; } = 10;
        public int? TargetAccount { get; } = null;
        public int? CategoryId { get; } = 42;
        public Recurrence Recurrence { get; } = Recurrence.Monthly;
        public bool IsLastDayOfMonth { get; } = true;
        public DateOnly LastRecurrence => StartDate;
    }

    public sealed class RecurringTransfer : IRecurringTransaction
    {
        public RecurringTransactionId Id => new(100);
        public DateOnly StartDate { get; } = new(year: 2023, month: 07, day: 12);
        public DateOnly? EndDate { get; } = new(year: 2050, month: 07, day: 12);
        public Money Amount { get; } = new(amount: 1042, currency: Currencies.CHF);
        public PaymentType Type => PaymentType.Transfer;
        public string? Note { get; } = null;
        public int ChargedAccount { get; } = 10;
        public int? TargetAccount { get; } = 12;
        public int? CategoryId { get; } = 42;
        public Recurrence Recurrence { get; } = Recurrence.Monthly;
        public bool IsLastDayOfMonth { get; } = true;
        public DateOnly LastRecurrence => StartDate;
    }

    public interface IRecurringTransaction
    {
        RecurringTransactionId Id { get; }

        DateOnly StartDate { get; }

        DateOnly? EndDate { get; }

        Money Amount { get; }

        PaymentType Type { get; }

        string? Note { get; }

        int ChargedAccount { get; }

        int? TargetAccount { get; }

        int? CategoryId { get; }

        Recurrence Recurrence { get; }

        bool IsLastDayOfMonth { get; }

        DateOnly LastRecurrence { get; }
    }
}
