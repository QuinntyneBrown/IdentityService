using IdentityService.Features.Core;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;

namespace IdentityService.Features.Accounts
{
    public interface IAccountsEventBusMessageHandler: IEventBusMessageHandler { }

    public class AccountsEventBusMessageHandler: IAccountsEventBusMessageHandler
    {
        public AccountsEventBusMessageHandler(ICache cache)
        {
            _cache = cache;
        }

        public void Handle(JObject message)
        {
            try
            {
                if ($"{message["Type"]}" == AccountsEventBusMessages.AddedOrUpdatedAccountMessage)
                {
                    _cache.Remove(AccountsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
                }

                if ($"{message["Type"]}" == AccountsEventBusMessages.RemovedAccountMessage)
                {
                    _cache.Remove(AccountsCacheKeyFactory.Get(new Guid(message["TenantUniqueId"].ToString())));
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
