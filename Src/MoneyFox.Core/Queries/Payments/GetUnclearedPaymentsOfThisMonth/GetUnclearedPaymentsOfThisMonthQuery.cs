﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyFox.Core._Pending_;
using MoneyFox.Core._Pending_.Common;
using MoneyFox.Core._Pending_.Common.Interfaces;
using MoneyFox.Core._Pending_.Common.QueryObjects;
using MoneyFox.Core.Aggregates.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyFox.Core.Queries.Payments.GetUnclearedPaymentsOfThisMonth
{
    public class GetUnclearedPaymentsOfThisMonthQuery : IRequest<List<Payment>>
    {
        public int AccountId { get; set; }

        public class Handler : IRequestHandler<GetUnclearedPaymentsOfThisMonthQuery, List<Payment>>
        {
            private readonly IContextAdapter contextAdapter;
            private readonly ISystemDateHelper systemDateHelper;

            public Handler(IContextAdapter contextAdapter, ISystemDateHelper systemDateHelper)
            {
                this.contextAdapter = contextAdapter;
                this.systemDateHelper = systemDateHelper;
            }

            public async Task<List<Payment>> Handle(GetUnclearedPaymentsOfThisMonthQuery request,
                CancellationToken cancellationToken)
            {
                IQueryable<Payment> query = contextAdapter.Context
                    .Payments
                    .Include(x => x.ChargedAccount)
                    .Include(x => x.TargetAccount)
                    .AreNotCleared()
                    .HasDateSmallerEqualsThan(
                        HelperFunctions.GetEndOfMonth(systemDateHelper));

                if(request.AccountId != 0)
                {
                    query = query.HasAccountId(request.AccountId);
                }

                return await query.ToListAsync(cancellationToken);
            }
        }
    }
}