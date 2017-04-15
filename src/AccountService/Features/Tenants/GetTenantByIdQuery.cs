using MediatR;
using AccountService.Data;
using AccountService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace AccountService.Features.Tenants
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
            public GetTenantByIdHandler(AccountServiceContext context, ICache cache)
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

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
