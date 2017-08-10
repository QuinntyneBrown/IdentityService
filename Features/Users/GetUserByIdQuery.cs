using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System;

namespace IdentityService.Features.Users
{
    public class GetUserByIdQuery
    {
        public class Request : IRequest<Response> { 
            public int Id { get; set; }
			public Guid? TenantUniqueId { get; set; }
        }

        public class Response
        {
            public UserApiModel User { get; set; } 
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
                    User = UserApiModel.FromUser(await _context.Users.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
