using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.Profiles
{
    public interface IProfilesEventBusMessageHandler: IEventBusMessageHandler { }

    public class ProfilesEventBusMessageHandler: IProfilesEventBusMessageHandler
    {
        public ProfilesEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == ProfilesEventBusMessages.AddedOrUpdatedProfileMessage)
                {
                    _cache.Remove(ProfilesCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == ProfilesEventBusMessages.RemovedProfileMessage)
                {
                    _cache.Remove(ProfilesCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
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
