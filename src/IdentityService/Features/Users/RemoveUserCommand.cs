using MediatR;
using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Users
{
    public class RemoveUserCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class Response { }

        public class RemoveUserHandler : IAsyncRequestHandler<Request, Response>
        {
            public RemoveUserHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var user = await _context.Users.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                user.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
