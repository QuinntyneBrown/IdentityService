using MediatR;
using AccountService.Data;
using AccountService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace AccountService.Features.Subscriptions
{
    public class GetSubscriptionByIdQuery
    {
        public class GetSubscriptionByIdRequest : IRequest<GetSubscriptionByIdResponse> { 
            public int Id { get; set; }
        }

        public class GetSubscriptionByIdResponse
        {
            public SubscriptionApiModel Subscription { get; set; } 
        }

        public class GetSubscriptionByIdHandler : IAsyncRequestHandler<GetSubscriptionByIdRequest, GetSubscriptionByIdResponse>
        {
            public GetSubscriptionByIdHandler(AccountServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSubscriptionByIdResponse> Handle(GetSubscriptionByIdRequest request)
            {                
                return new GetSubscriptionByIdResponse()
                {
                    Subscription = SubscriptionApiModel.FromSubscription(await _context.Subscriptions
					.SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
