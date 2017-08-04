using MediatR;
using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Subscriptions
{
    public class RemoveSubscriptionCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response { }

        public class RemoveSubscriptionHandler : IAsyncRequestHandler<Request, Response>
        {
            public RemoveSubscriptionHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var subscription = await _context.Subscriptions.SingleAsync(x=>x.Id == request.Id);
                subscription.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
