using IdentityService.Data;
using IdentityService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace IdentityService.Features.Tenants
{
    public class RemoveTenantCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
            public Guid CorrelationId { get; set; }
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
                var tenant = await _context.Tenants.SingleAsync(x=>x.Id == request.Id);
                tenant.IsDeleted = true;
                await _context.SaveChangesAsync();

                _bus.Publish(new RemovedTenantMessage(tenant.Id, request.CorrelationId));

                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
