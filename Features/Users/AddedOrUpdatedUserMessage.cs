using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Users
{

    public class AddedOrUpdatedUserMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedUserMessage(User user, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = user, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = UsersEventBusMessages.AddedOrUpdatedUserMessage;        
    }
}
