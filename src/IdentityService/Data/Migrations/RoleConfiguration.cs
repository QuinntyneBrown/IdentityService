using System.Data.Entity.Migrations;
using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Features.Users;

namespace IdentityService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(IdentityServiceContext context) {

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
