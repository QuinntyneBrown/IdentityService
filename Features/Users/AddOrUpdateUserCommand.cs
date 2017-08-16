using MediatR;
using IdentityService.Data;
using IdentityService.Model;
using IdentityService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace IdentityService.Features.Users
{
    public class AddOrUpdateUserCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public UserApiModel User { get; set; }
            public Guid CorrelationId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, IEventBus bus)
            {
                _context = context;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.Users
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.User.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                if (entity == null) _context.Users.Add(entity = new User());

                entity.Name = request.User.Name;

                await _context.SaveChangesAsync();

                _bus.Publish(new AddedOrUpdatedUserMessage(entity,request.CorrelationId,request.TenantUniqueId));

                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
