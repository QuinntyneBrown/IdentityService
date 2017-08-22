using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Users
{
    public class PasswordChanagedMessage : BaseEventBusMessage
    {
        public PasswordChanagedMessage(UserApiModel user, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = user, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = UsersEventBusMessages.PasswordChangedMessage;
    }
}