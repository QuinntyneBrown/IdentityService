using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Subscriptions
{

    public class AddedOrUpdatedSubscriptionMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedSubscriptionMessage(Subscription subscription, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = subscription, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = SubscriptionsEventBusMessages.AddedOrUpdatedSubscriptionMessage;        
    }
}
