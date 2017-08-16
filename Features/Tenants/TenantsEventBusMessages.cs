namespace IdentityService.Features.Tenants
{
    public class TenantsEventBusMessages
    {
        public static string AddedOrUpdatedTenantMessage = "[Tenants] TenantAddedOrUpdated";
        public static string RemovedTenantMessage = "[Tenants] TenantRemoved";
    }
}
