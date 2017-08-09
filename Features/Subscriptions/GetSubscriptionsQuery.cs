using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Subscriptions
{
    public class GetSubscriptionsQuery
    {
        public class Request : IRequest<Response> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class Response
        {
            public ICollection<SubscriptionApiModel> Subscriptions { get; set; } = new HashSet<SubscriptionApiModel>();
        }

        public class GetSubscriptionsHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetSubscriptionsHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var subscriptions = await _context.Subscriptions
                    .ToListAsync();

                return new Response()
                {
                    Subscriptions = subscriptions.Select(x => SubscriptionApiModel.FromSubscription(x)).ToList()
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
