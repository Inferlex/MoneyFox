﻿namespace MoneyFox.Core.ApplicationCore.UseCases.BudgetCreation
{

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Aggregates.BudgetAggregate;
    using MediatR;

    internal sealed class CreateBudget
    {
        public class Query : IRequest
        {
            public Query(string name, double spendingLimit, List<int> categories)
            {
                Name = name;
                SpendingLimit = spendingLimit;
                Categories = categories;
            }

            public string Name { get; }
            public double SpendingLimit { get; }
            public List<int> Categories { get; }
        }

        public class Handler : IRequestHandler<Query>
        {
            private IBudgetRepository repository;

            public Handler(IBudgetRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                var budget = new Budget(request.Name, request.SpendingLimit, request.Categories);
                await repository.AddAsync(budget);

                return Unit.Value;
            }
        }
    }

}
