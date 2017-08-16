using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Subscriptions
{
    public class RemovedSubscriptionMessage : BaseEventBusMessage
    {
        public RemovedSubscriptionMessage(int subscriptionId, Guid correlationId, Guid tenantId)
        {
            Payload = new { Id = subscriptionId, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = SubscriptionsEventBusMessages.RemovedSubscriptionMessage;        
    }
}
