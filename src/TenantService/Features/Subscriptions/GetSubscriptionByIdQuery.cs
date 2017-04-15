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
    public class GetSubscriptionByIdQuery
    {
        public class GetSubscriptionByIdRequest : IRequest<GetSubscriptionByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetSubscriptionByIdResponse
        {
            public SubscriptionApiModel Subscription { get; set; } 
        }

        public class GetSubscriptionByIdHandler : IAsyncRequestHandler<GetSubscriptionByIdRequest, GetSubscriptionByIdResponse>
        {
            public GetSubscriptionByIdHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSubscriptionByIdResponse> Handle(GetSubscriptionByIdRequest request)
            {                
                return new GetSubscriptionByIdResponse()
                {
                    Subscription = SubscriptionApiModel.FromSubscription(await _context.Subscriptions
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
