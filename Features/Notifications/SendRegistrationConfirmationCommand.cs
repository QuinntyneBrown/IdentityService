using IdentityService.Data;
using IdentityService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;

namespace IdentityService.Features.Notifications
{
    public class SendRegistrationConfirmationCommand
    {
        public class Request : BaseRequest, IRequest<SendRegistrationConfirmationResponse> { }

        public class SendRegistrationConfirmationResponse { }

        public class SendRegistrationConfirmationHandler : IAsyncRequestHandler<Request, SendRegistrationConfirmationResponse>
        {
            public SendRegistrationConfirmationHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<SendRegistrationConfirmationResponse> Handle(Request request)
            {
                throw new System.NotImplementedException();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}