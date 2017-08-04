using IdentityService.Data;
using IdentityService.Data.Model;
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
        }

        public class Response { }

        public class AddOrUpdateTenantHandler : IAsyncRequestHandler<Request, Response>
        {
            public AddOrUpdateTenantHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
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

                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
