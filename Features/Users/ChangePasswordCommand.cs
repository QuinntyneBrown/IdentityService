using IdentityService.Data;
using IdentityService.Features.Core;
using IdentityService.Features.Security;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Users
{
    public class ChangePasswordCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public string Password { get; set; }

            public string ConfirmPassword { get; set; }

            public UserApiModel User { get; set; }

            public Guid CorrelationId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IIdentityServiceContext context, IEventBus bus, IEncryptionService encryptionService, IChangePasswordCommandValidator validator)
            {
                _context = context;
                _encryptionService = encryptionService;
                _bus = bus;
                _validator = validator;
            }

            public async Task<Response> Handle(Request request)
            {
                try
                {
                    var results = _validator.Validate(request);

                    if (!results.IsValid)
                        throw new Exception(results.Errors.First().ErrorMessage);

                    var user = await _context.Users
                        .Include(x => x.Tenant)
                        .SingleAsync(x => x.Id == request.User.Id);

                    user.Password = _encryptionService.TransformPassword(request.Password);

                    await _context.SaveChangesAsync();

                    return new Response()
                    {

                    };
                }
                catch (Exception exception) {
                    throw exception;
                }
            }

            private readonly IIdentityServiceContext _context;
            private readonly IEncryptionService _encryptionService;
            private readonly IEventBus _bus;
            private readonly IChangePasswordCommandValidator _validator;
        }
    }
}
