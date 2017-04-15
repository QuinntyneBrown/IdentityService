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
    public class AddOrUpdateSubscriptionCommand
    {
        public class AddOrUpdateSubscriptionRequest : IRequest<AddOrUpdateSubscriptionResponse>
        {
            public SubscriptionApiModel Subscription { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateSubscriptionResponse { }

        public class AddOrUpdateSubscriptionHandler : IAsyncRequestHandler<AddOrUpdateSubscriptionRequest, AddOrUpdateSubscriptionResponse>
        {
            public AddOrUpdateSubscriptionHandler(AccountServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateSubscriptionResponse> Handle(AddOrUpdateSubscriptionRequest request)
            {
                var entity = await _context.Subscriptions
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Subscription.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Subscriptions.Add(entity = new Subscription() { TenantId = tenant.Id });
                }

                entity.Name = request.Subscription.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateSubscriptionResponse();
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
