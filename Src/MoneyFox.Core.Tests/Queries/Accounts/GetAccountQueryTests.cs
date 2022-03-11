﻿namespace MoneyFox.Core.Tests.Queries.Accounts
{
    using Common.Interfaces;
    using Core.Aggregates;
    using Core.Queries.Accounts.GetAccounts;
    using Infrastructure;
    using MoneyFox.Infrastructure.Persistence;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class GetAccountQueryTests : IDisposable
    {
        private readonly AppDbContext context;
        private readonly Mock<IContextAdapter> contextAdapterMock;

        public GetAccountQueryTests()
        {
            context = InMemoryAppDbContextFactory.Create();

            contextAdapterMock = new Mock<IContextAdapter>();
            contextAdapterMock.SetupGet(x => x.Context).Returns(context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => InMemoryAppDbContextFactory.Destroy(context);

        [Fact]
        public async Task GetAccountQuery_CorrectNumberLoaded()
        {
            // Arrange
            var account = new Account("test", 80);
            await context.AddAsync(account);
            await context.SaveChangesAsync();

            // Act
            List<Account> result =
                await new GetAccountsQuery.Handler(contextAdapterMock.Object).Handle(new GetAccountsQuery(), default);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task DontLoadDeactivatedAccounts()
        {
            // Arrange
            var account1 = new Account("test", 80);
            var account2 = new Account("test", 80);

            account2.Deactivate();

            await context.AddAsync(account1);
            await context.AddAsync(account2);
            await context.SaveChangesAsync();

            // Act
            List<Account> result =
                await new GetAccountsQuery.Handler(contextAdapterMock.Object).Handle(new GetAccountsQuery(), default);

            // Assert
            Assert.Single(result);
        }
    }
}