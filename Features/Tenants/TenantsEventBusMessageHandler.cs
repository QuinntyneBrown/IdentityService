using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.Tenants
{
    public interface ITenantsEventBusMessageHandler: IEventBusMessageHandler { }

    public class TenantsEventBusMessageHandler: ITenantsEventBusMessageHandler
    {
        public TenantsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == TenantsEventBusMessages.AddedOrUpdatedTenantMessage)
                {
                    _cache.Remove(TenantsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == TenantsEventBusMessages.RemovedTenantMessage)
                {
                    _cache.Remove(TenantsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
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
