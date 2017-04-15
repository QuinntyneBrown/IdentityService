using AccountService.Data;
using AccountService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AccountService.Features.Notifications
{
    public class SendRegistrationConfirmationCommand
    {
        public class SendRegistrationConfirmationRequest : IRequest<SendRegistrationConfirmationResponse>
        {
            public Guid TenantUniqueId { get; set; }
        }

        public class SendRegistrationConfirmationResponse { }

        public class SendRegistrationConfirmationHandler : IAsyncRequestHandler<SendRegistrationConfirmationRequest, SendRegistrationConfirmationResponse>
        {
            public SendRegistrationConfirmationHandler(AccountServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<SendRegistrationConfirmationResponse> Handle(SendRegistrationConfirmationRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }
    }
}