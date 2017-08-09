using System;

namespace IdentityService.Features.Core
{
    public class BaseRequest
    {
        public Guid TenantUniqueId { get; set; }
    }
}
