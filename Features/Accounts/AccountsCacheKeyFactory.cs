using System;

namespace IdentityService.Features.Accounts
{
    public class AccountsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[Accounts] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[Accounts] GetById {tenantId}-{id}";
    }
}
