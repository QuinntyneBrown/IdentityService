using System;

namespace IdentityService.Features.Subscriptions
{
    public class SubscriptionsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Subscriptions] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Subscriptions] GetById {tenantId}-{id}";
    }
}
