using MediatR;
using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Subscriptions
{
    public class AddOrUpdateSubscriptionCommand
    {
        public class Request : IRequest<Response>
        {
            public SubscriptionApiModel Subscription { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.Subscriptions
                    .SingleOrDefaultAsync(x => x.Id == request.Subscription.Id);
                
                if (entity == null) {
                    _context.Subscriptions.Add(entity = new Subscription() { });
                }

                entity.Name = request.Subscription.Name;

                entity.EffectiveDate = request.Subscription.EffectiveDate;

                entity.ExpiresOn = request.Subscription.ExpiresOn;

                entity.FeatureId = request.Subscription.FeatureId;

                entity.AccountId = request.Subscription.AccountId;

                await _context.SaveChangesAsync();

                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}