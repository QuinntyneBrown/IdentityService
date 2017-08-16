using IdentityService.Model;
using IdentityService.Features.Core;
using System;

namespace IdentityService.Features.Accounts
{

    public class AddedOrUpdatedAccountMessage : BaseEventBusMessage
    {
        public AddedOrUpdatedAccountMessage(Account account, Guid correlationId, Guid tenantId)
        {
            Payload = new { Entity = account, CorrelationId = correlationId };
            TenantUniqueId = tenantId;
        }
        public override string Type { get; set; } = AccountsEventBusMessages.AddedOrUpdatedAccountMessage;        
    }
}
