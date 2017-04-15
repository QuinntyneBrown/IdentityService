using MediatR;
using AccountService.Data;
using AccountService.Data.Model;
using AccountService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace AccountService.Features.Subscriptions
{
    public class RemoveSubscriptionCommand
    {
        public class RemoveSubscriptionRequest : IRequest<RemoveSubscriptionResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveSubscriptionResponse { }

        public class RemoveSubscriptionHandler : IAsyncRequestHandler<RemoveSubscriptionRequest, RemoveSubscriptionResponse>
        {
            public RemoveSubscriptionHandler(AccountServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveSubscriptionResponse> Handle(RemoveSubscriptionRequest request)
            {
                var subscription = await _context.Subscriptions.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                subscription.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveSubscriptionResponse();
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
