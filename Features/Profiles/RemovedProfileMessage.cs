using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Profiles
{
    public class RemovedProfileMessage : BaseEventBusMessage
    {
        public RemovedProfileMessage(int profileId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = profileId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = ProfilesEventBusMessages.RemovedProfileMessage;        
    }
}
