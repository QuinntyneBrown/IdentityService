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
    public class GetTenantByIdQuery
    {
        public class GetTenantByIdRequest : IRequest<GetTenantByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetTenantByIdResponse
        {
            public TenantApiModel Tenant { get; set; } 
        }

        public class GetTenantByIdHandler : IAsyncRequestHandler<GetTenantByIdRequest, GetTenantByIdResponse>
        {
            public GetTenantByIdHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTenantByIdResponse> Handle(GetTenantByIdRequest request)
            {                
                return new GetTenantByIdResponse()
                {
                    Tenant = TenantApiModel.FromTenant(await _context.Tenants                    		
					.SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
