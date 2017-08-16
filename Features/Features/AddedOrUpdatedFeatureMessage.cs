using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Features
{

    public class AddedOrUpdatedFeatureMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedFeatureMessage(Feature feature, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = feature, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = FeaturesEventBusMessages.AddedOrUpdatedFeatureMessage;        
    }
}
