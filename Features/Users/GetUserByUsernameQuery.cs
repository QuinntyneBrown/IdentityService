using IdentityService.Data;
using IdentityService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Users
{
    public class GetUserByUsernameQuery
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public string Username { get; set; }
        }

        public class Response
        {
            public UserApiModel User { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IIdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response()
                {
                    User = UserApiModel.FromUser(await _context.Users.SingleAsync(x=>x.Username == request.Username && x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly IIdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}