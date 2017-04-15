using System.Data.Entity.Migrations;
using TenantService.Data;
using TenantService.Data.Model;
using TenantService.Features.Users;

namespace TenantService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(TenantServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.ACCOUNT_HOLDER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
