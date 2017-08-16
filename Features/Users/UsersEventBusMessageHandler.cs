using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.Users
{
    public interface IUsersEventBusMessageHandler: IEventBusMessageHandler { }

    public class UsersEventBusMessageHandler: IUsersEventBusMessageHandler
    {
        public UsersEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == UsersEventBusMessages.AddedOrUpdatedUserMessage)
                {
                    _cache.Remove(UsersCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == UsersEventBusMessages.RemovedUserMessage)
                {
                    _cache.Remove(UsersCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private readonly ICache _cache;
    }
}
