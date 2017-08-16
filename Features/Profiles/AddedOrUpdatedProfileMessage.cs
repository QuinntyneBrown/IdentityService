using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Profiles
{

    public class AddedOrUpdatedProfileMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedProfileMessage(Profile profile, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = profile, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = ProfilesEventBusMessages.AddedOrUpdatedProfileMessage;        
    }
}
