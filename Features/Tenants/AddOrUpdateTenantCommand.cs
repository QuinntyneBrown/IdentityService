using IdentityService.Data;
using IdentityService.Model;
using IdentityService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Tenants
{
    public class AddOrUpdateTenantCommand
    {
        public class Request : IRequest<Response>
        {
            public TenantApiModel Tenant { get; set; }
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
                var entity = await _context.Tenants                    
                    .SingleOrDefaultAsync(x => x.Id == request.Tenant.Id);
                
                if (entity == null) {                    
                    _context.Tenants.Add(entity = new Tenant() { });
                }

                entity.Name = request.Tenant.Name;

                entity.HostUrl = request.Tenant.HostUrl;

                entity.UniqueId = new Guid(request.Tenant.UniqueId);

                await _context.SaveChangesAsync();

                _bus.Publish(new AddedOrUpdatedTenantMessage(entity,request.CorrelationId));

                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
