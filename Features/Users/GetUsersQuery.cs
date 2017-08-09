using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Users
{
    public class GetUsersQuery
    {
        public class Request : IRequest<Response> { 
            public int? TenantId { get; set; }		
		}

        public class Response
        {
            public ICollection<UserApiModel> Users { get; set; } = new HashSet<UserApiModel>();
        }

        public class GetUsersHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetUsersHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var users = await _context.Users
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new Response()
                {
                    Users = users.Select(x => UserApiModel.FromUser(x)).ToList()
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
