using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Users
{
    public class RemovedUserMessage : BaseEventBusMessage
    {
        public RemovedUserMessage(int userId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = userId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = UsersEventBusMessages.RemovedUserMessage;        
    }
}
