using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AccountService.Data;
using AccountService.Data.Model;
using AccountService.Security;

namespace AccountService.Migrations
{
    public class UserConfiguration
    {
        public static void Seed(AccountServiceContext context) {

            var systemRole = context.Roles.First(x => x.Name == Roles.SYSTEM);
            var roles = new List<Role>();
            var tenant = context.Tenants.Single(x => x.Name == "Default");

            roles.Add(systemRole);

            context.Users.AddOrUpdate(x => x.Username, new User()
            {
                Username = "system",
                Password = new EncryptionService().TransformPassword("system"),
                Roles = roles,
                TenantId = tenant.Id
            });
                        
            context.SaveChanges();
        }
    }
}
