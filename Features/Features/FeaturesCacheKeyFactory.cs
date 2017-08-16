using System;

namespace IdentityService.Features.Features
{
    public class FeaturesCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Features] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Features] GetById {tenantId}-{id}";
    }
}
