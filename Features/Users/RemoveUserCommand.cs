using MediatR;
using IdentityService.Data;
using IdentityService.Model;
using IdentityService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System;

namespace IdentityService.Features.Users
{
    public class RemoveUserCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public int Id { get; set; }
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
                var user = await _context.Users
                    .Include(x=>x.Tenant)
                    .SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);

                user.IsDeleted = true;

                await _context.SaveChangesAsync();
                _bus.Publish(new RemovedUserMessage(request.Id,request.CorrelationId, request.TenantUniqueId));
                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
