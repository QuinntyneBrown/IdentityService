using System.Data.Entity.Migrations;
using AccountService.Data;
using AccountService.Data.Model;
using AccountService.Features.Users;

namespace AccountService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(AccountServiceContext context) {

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
