using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System;
using System.Threading.Tasks;

namespace IdentityService.Features.Security
{
    public class GetSymetricKeyQuery
    {
        public class Request : IRequest<Response> { }

        public class Response {
            public string Key { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, ICache cache, Lazy<IAuthConfiguration> lazyAuthConfiguration)
            {
                _context = context;
                _cache = cache;
                _authConfiguration = lazyAuthConfiguration.Value;
            }

            public async Task<Response> Handle(Request request)
            {
                return new Response()
                {
                    Key = _authConfiguration.JwtKey
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
            private IAuthConfiguration _authConfiguration;
        }
    }
}
