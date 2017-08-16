using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.DigitalAssets
{
    public interface IDigitalAssetsEventBusMessageHandler: IEventBusMessageHandler { }

    public class DigitalAssetsEventBusMessageHandler: IDigitalAssetsEventBusMessageHandler
    {
        public DigitalAssetsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == DigitalAssetsEventBusMessages.AddedOrUpdatedDigitalAssetMessage)
                {
                    _cache.Remove(DigitalAssetsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == DigitalAssetsEventBusMessages.RemovedDigitalAssetMessage)
                {
                    _cache.Remove(DigitalAssetsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
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
