using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Users
{
    public class GetUserByIdQuery
    {
        public class Request : BaseRequest, IRequest<Response> { 
            public int Id { get; set; }
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
                    User = UserApiModel.FromUser(await _context.Users.SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
