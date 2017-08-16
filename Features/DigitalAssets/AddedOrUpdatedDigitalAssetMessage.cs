using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.DigitalAssets
{

    public class AddedOrUpdatedDigitalAssetMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedDigitalAssetMessage(DigitalAsset digitalAsset, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = digitalAsset, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = DigitalAssetsEventBusMessages.AddedOrUpdatedDigitalAssetMessage;        
    }
}
