﻿using MoneyFox.Application.Common.QueryObjects;
using MoneyFox.Domain.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace MoneyFox.Application.Tests.QueryObjects;

[ExcludeFromCodeCoverage]
public class AccountQueriesExtensionsTests
{
    [Fact]
    public void AreActive()
    {
        // Arrange
        var accountQueryList = new List<Account> {new("Foo1"), new("Foo2"), new("absd")};

        accountQueryList[1].Deactivate();

        // Act
        var resultList = accountQueryList.AsQueryable().AreActive().ToList();

        // Assert
        Assert.Equal(2, resultList.Count);
        Assert.Equal("Foo1", resultList[0].Name);
        Assert.Equal("absd", resultList[1].Name);
    }
}