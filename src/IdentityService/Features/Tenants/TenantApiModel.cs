using IdentityService.Data.Model;

namespace IdentityService.Features.Tenants
{
    public class TenantApiModel
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public string UniqueId { get; set; }

        public string HostUrl { get; set; }

        public static TModel FromTenant<TModel>(Tenant tenant) where
            TModel : TenantApiModel, new()
        {
            var model = new TModel();

            model.Id = tenant.Id;

            model.Name = tenant.Name;

            model.HostUrl = tenant.HostUrl;

            model.UniqueId = $"{tenant.UniqueId}";

            return model;
        }

        public static TenantApiModel FromTenant(Tenant tenant)
            => FromTenant<TenantApiModel>(tenant);

    }
}
