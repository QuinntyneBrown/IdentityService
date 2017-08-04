using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Tenants
{
    public class GetTenantsQuery
    {
        public class Request : IRequest<Response> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class Response
        {
            public ICollection<TenantApiModel> Tenants { get; set; } = new HashSet<TenantApiModel>();
        }

        public class GetTenantsHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetTenantsHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var tenants = await _context.Tenants                    
                    .ToListAsync();

                return new Response()
                {
                    Tenants = tenants.Select(x => TenantApiModel.FromTenant(x)).ToList()
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
