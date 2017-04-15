using MediatR;
using TenantService.Data;
using TenantService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TenantService.Features.Subscriptions
{
    public class GetSubscriptionsQuery
    {
        public class GetSubscriptionsRequest : IRequest<GetSubscriptionsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetSubscriptionsResponse
        {
            public ICollection<SubscriptionApiModel> Subscriptions { get; set; } = new HashSet<SubscriptionApiModel>();
        }

        public class GetSubscriptionsHandler : IAsyncRequestHandler<GetSubscriptionsRequest, GetSubscriptionsResponse>
        {
            public GetSubscriptionsHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSubscriptionsResponse> Handle(GetSubscriptionsRequest request)
            {
                var subscriptions = await _context.Subscriptions
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetSubscriptionsResponse()
                {
                    Subscriptions = subscriptions.Select(x => SubscriptionApiModel.FromSubscription(x)).ToList()
                };
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
