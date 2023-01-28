﻿namespace MoneyFox.Core.Tests.ApplicationCore.Queries.Statistics;

using System.Diagnostics.CodeAnalysis;
using Core.ApplicationCore.Domain.Aggregates.AccountAggregate;
using Core.ApplicationCore.Queries.Statistics;
using FluentAssertions;
using Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
[Collection("CultureCollection")]
public class GetAccountProgressionHandlerTests
{
    private readonly AppDbContext context;
    private readonly GetAccountProgressionHandler getAccountProgressionHandler;

    public GetAccountProgressionHandlerTests()
    {
        context = InMemoryAppDbContextFactory.Create();
        getAccountProgressionHandler = new(context);
    }

    [Fact]
    public async Task CalculateCorrectSums()
    {
        // Arrange
        var account = new Account("Foo1");
        context.AddRange(
            new List<Payment>
            {
                new(date: DateTime.Today, amount: 60, type: PaymentType.Income, chargedAccount: account),
                new(date: DateTime.Today, amount: 20, type: PaymentType.Expense, chargedAccount: account),
                new(date: DateTime.Today.AddMonths(-1), amount: 50, type: PaymentType.Expense, chargedAccount: account),
                new(date: DateTime.Today.AddMonths(-2), amount: 40, type: PaymentType.Expense, chargedAccount: account)
            });

        context.Add(account);
        context.SaveChanges();

        // Act
        var result = await getAccountProgressionHandler.Handle(
            request: new(accountId: account.Id, startDate: DateTime.Today.AddYears(-1), endDate: DateTime.Today.AddDays(3)),
            cancellationToken: default);

        // Assert
        result[0].Value.Should().Be(40);
        result[1].Value.Should().Be(-50);
        result[2].Value.Should().Be(-40);
    }

    [Fact]
    public async Task GetValues_CorrectSums()
    {
        // Arrange
        var account = new Account("Foo1");
        context.AddRange(
            new List<Payment>
            {
                new(date: DateTime.Today, amount: 60, type: PaymentType.Income, chargedAccount: account),
                new(date: DateTime.Today, amount: 20, type: PaymentType.Expense, chargedAccount: account),
                new(date: DateTime.Today.AddMonths(-1), amount: 50, type: PaymentType.Expense, chargedAccount: account),
                new(date: DateTime.Today.AddMonths(-2), amount: 40, type: PaymentType.Expense, chargedAccount: account)
            });

        context.Add(account);
        context.SaveChanges();

        // Act
        var result = await getAccountProgressionHandler.Handle(
            request: new(accountId: account.Id, startDate: DateTime.Today.AddYears(-1), endDate: DateTime.Today.AddDays(3)),
            cancellationToken: default);

        // Assert
        result[0].Color.Should().Be("#87cefa");
        result[1].Color.Should().Be("#cd3700");
        result[2].Color.Should().Be("#cd3700");
    }
}
