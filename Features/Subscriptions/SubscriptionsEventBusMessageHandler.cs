using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.Subscriptions
{
    public interface ISubscriptionsEventBusMessageHandler: IEventBusMessageHandler { }

    public class SubscriptionsEventBusMessageHandler: ISubscriptionsEventBusMessageHandler
    {
        public SubscriptionsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == SubscriptionsEventBusMessages.AddedOrUpdatedSubscriptionMessage)
                {
                    _cache.Remove(SubscriptionsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == SubscriptionsEventBusMessages.RemovedSubscriptionMessage)
                {
                    _cache.Remove(SubscriptionsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
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
