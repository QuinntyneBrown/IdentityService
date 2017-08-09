using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Users
{
    public class GetUserByUsernameQuery
    {
        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public int? TenantId { get; set; }
        }

        public class Response
        {
            public UserApiModel User { get; set; }
        }

        public class GetUserByUsernameHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetUserByUsernameHandler(IIdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response()
                {
                    User = UserApiModel.FromUser(await _context.Users.SingleAsync(x=>x.Username == request.Username && x.TenantId == request.TenantId))
                };
            }

            private readonly IIdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}