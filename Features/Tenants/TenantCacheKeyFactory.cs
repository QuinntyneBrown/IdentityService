using System;

namespace IdentityService.Features.Tenants
{
    public class TenantsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Tenants] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Tenants] GetById {tenantId}-{id}";
    }
}
