using System;

namespace IdentityService.Features.Users
{
    public class UsersCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Users] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Users] GetById {tenantId}-{id}";
    }
}
