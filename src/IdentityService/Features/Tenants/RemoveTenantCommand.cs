using MediatR;
using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Tenants
{
    public class RemoveTenantCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class Response { }

        public class RemoveTenantHandler : IAsyncRequestHandler<Request, Response>
        {
            public RemoveTenantHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var tenant = await _context.Tenants.SingleAsync(x=>x.Id == request.Id);
                tenant.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
