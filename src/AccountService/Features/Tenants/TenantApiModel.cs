using AccountService.Data.Model;

namespace AccountService.Features.Tenants
{
    public class TenantApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromTenant<TModel>(Tenant tenant) where
            TModel : TenantApiModel, new()
        {
            var model = new TModel();
            model.Id = tenant.Id;            
            model.Name = tenant.Name;
            return model;
        }

        public static TenantApiModel FromTenant(Tenant tenant)
            => FromTenant<TenantApiModel>(tenant);

    }
}
