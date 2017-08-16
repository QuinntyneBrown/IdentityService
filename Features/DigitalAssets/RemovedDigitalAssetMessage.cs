using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.DigitalAssets
{
    public class RemovedDigitalAssetMessage : BaseEventBusMessage
    {
        public RemovedDigitalAssetMessage(int digitalAssetId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = digitalAssetId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = DigitalAssetsEventBusMessages.RemovedDigitalAssetMessage;        
    }
}
