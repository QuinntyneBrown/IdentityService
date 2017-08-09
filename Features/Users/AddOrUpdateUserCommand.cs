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
    public class AddOrUpdateUserCommand
    {
        public class Request : IRequest<Response>
        {
            public UserApiModel User { get; set; }
			public int? TenantId { get; set; }
        }

        public class Response { }

        public class AddOrUpdateUserHandler : IAsyncRequestHandler<Request, Response>
        {
            public AddOrUpdateUserHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.Users
                    .SingleOrDefaultAsync(x => x.Id == request.User.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Users.Add(entity = new User());
                entity.Name = request.User.Name;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
