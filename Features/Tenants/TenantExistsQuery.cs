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
    public class TenantExistsQuery
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public Guid UniqueId { get; set; }
        }

        public class Response
        {
            public bool Exists { get; set; } 
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response()
                {
                    Exists = await _context.Tenants.SingleOrDefaultAsync(x => x.UniqueId == request.UniqueId) != null
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
