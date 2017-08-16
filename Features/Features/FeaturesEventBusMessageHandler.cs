using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.Features
{
    public interface IFeaturesEventBusMessageHandler: IEventBusMessageHandler { }

    public class FeaturesEventBusMessageHandler: IFeaturesEventBusMessageHandler
    {
        public FeaturesEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == FeaturesEventBusMessages.AddedOrUpdatedFeatureMessage)
                {
                    _cache.Remove(FeaturesCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == FeaturesEventBusMessages.RemovedFeatureMessage)
                {
                    _cache.Remove(FeaturesCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
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
