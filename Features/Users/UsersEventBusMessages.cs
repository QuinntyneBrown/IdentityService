namespace IdentityService.Features.Users
{
    public class UsersEventBusMessages
    {
        public static string AddedOrUpdatedUserMessage = "[Users] UserAddedOrUpdated";
        public static string RemovedUserMessage = "[Users] UserRemoved";
        public static string PasswordChangedMessage = "[Users] PasswordChangedMessage";
    }
}
