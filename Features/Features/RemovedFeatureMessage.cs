using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Features
{
    public class RemovedFeatureMessage : BaseEventBusMessage
    {
        public RemovedFeatureMessage(int featureId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = featureId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = FeaturesEventBusMessages.RemovedFeatureMessage;        
    }
}
