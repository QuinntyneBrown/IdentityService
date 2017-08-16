using System;

namespace IdentityService.Features.Notifications
{
    public class NotificationsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Notifications] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Notifications] GetById {tenantId}-{id}";
    }
}
