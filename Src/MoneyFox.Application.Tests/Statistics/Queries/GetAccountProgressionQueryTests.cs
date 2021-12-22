﻿using FluentAssertions;
using MoneyFox.Application.Statistics.Queries;
using MoneyFox.Domain.Exceptions;
using System;
using Xunit;

namespace MoneyFox.Application.Tests.Statistics.Queries
{
    public class GetAccountProgressionQueryTests
    {
        [Fact]
        public void ExceptionOnInvalidDates() =>
            // Arrange
            // Act / Assert
            Assert.Throws<StartAfterEnddateException>(
                () =>
                    new GetAccountProgressionQuery(0, DateTime.Today.AddYears(3), DateTime.Today));

        [Fact]
        public void NoExceptionOnSameDate()
        {
            // Arrange
            DateTime date = DateTime.Now;

            // Act
            var query = new GetAccountProgressionQuery(0, date, date);

            // Assert
            query.Should().NotBeNull();
        }
    }
}