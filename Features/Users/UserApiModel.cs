using IdentityService.Data.Model;

namespace IdentityService.Features.Users
{
    public class UserApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public static TModel FromUser<TModel>(User user) where
            TModel : UserApiModel, new()
        {
            var model = new TModel();

            model.Id = user.Id;

            model.TenantId = user.TenantId;

            model.Username = user.Username;

            return model;
        }

        public static UserApiModel FromUser(User user)
            => FromUser<UserApiModel>(user);

    }
}
