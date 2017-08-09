using IdentityService.Data;
using IdentityService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;

namespace IdentityService.Security
{
    public class AddSessionCommand
    {
        public class Request : IRequest<Response>
        {
            public Guid TenantUniqueId { get; set; }
            public DateTimeOffset? StartedOn { get; set; }
            public DateTimeOffset? ExpiresOn { get; set; }
            public string AccessToken { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, IEventBus bus)
            {
                _context = context;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}