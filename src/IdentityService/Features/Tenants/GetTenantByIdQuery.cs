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
    public class GetTenantByIdQuery
    {
        public class Request : IRequest<Response> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class Response
        {
            public TenantApiModel Tenant { get; set; } 
        }

        public class GetTenantByIdHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetTenantByIdHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {                
                return new Response()
                {
                    Tenant = TenantApiModel.FromTenant(await _context.Tenants                    		
					.SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
