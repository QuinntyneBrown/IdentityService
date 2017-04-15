using MediatR;
using TenantService.Data;
using TenantService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TenantService.Features.Tenants
{
    public class GetTenantsQuery
    {
        public class GetTenantsRequest : IRequest<GetTenantsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetTenantsResponse
        {
            public ICollection<TenantApiModel> Tenants { get; set; } = new HashSet<TenantApiModel>();
        }

        public class GetTenantsHandler : IAsyncRequestHandler<GetTenantsRequest, GetTenantsResponse>
        {
            public GetTenantsHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTenantsResponse> Handle(GetTenantsRequest request)
            {
                var tenants = await _context.Tenants                    
                    .ToListAsync();

                return new GetTenantsResponse()
                {
                    Tenants = tenants.Select(x => TenantApiModel.FromTenant(x)).ToList()
                };
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
