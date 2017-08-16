using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Tenants
{
    public class RemovedTenantMessage : BaseEventBusMessage
    {
        public RemovedTenantMessage(int tenantId, Guid correlationId)
        {
            Payload = new { Id = tenantId, CorrelationId = correlationId };
        }
        public override string Type { get; set; } = TenantsEventBusMessages.RemovedTenantMessage;        
    }
}
