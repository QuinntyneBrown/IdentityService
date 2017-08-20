using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using IdentityService.Features.Security;

namespace IdentityService.Features.Users
{
    public class ChangePasswordCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public string Password { get; set; }

            public string ConfirmPassword { get; set; }

            public UserApiModel User { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, IEventBus bus, IEncryptionService encryptionService)
            {
                _context = context;
                _encryptionService = encryptionService;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                if (request.ConfirmPassword != request.Password)
                    throw new Exception();

                var user = await _context.Users
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.Id == request.User.Id);

                user.Password = _encryptionService.TransformPassword(request.Password);

                await _context.SaveChangesAsync();

                return new Response()
                {

                };
            }

            private readonly IdentityServiceContext _context;
            private readonly IEncryptionService _encryptionService;
            private readonly IEventBus _bus;
        }

    }
}
