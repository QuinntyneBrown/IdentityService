using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Tenants
{

    public class AddedOrUpdatedTenantMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedTenantMessage(Tenant tenant, Guid correlationId)
        {
            Payload = new { Entity = tenant, CorrelationId = correlationId };
        }
        public override string Type { get; set; } = TenantsEventBusMessages.AddedOrUpdatedTenantMessage;        
    }
}
