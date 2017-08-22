using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityService.Features.Core
{
    public class BaseAuthenticatedRequest: BaseRequest
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public int UserId { get
            {
                return Convert.ToInt16(ClaimsPrincipal?.Claims.Single(x => x.Type == Security.ClaimTypes.UserId).Value);
            }
        }

        public string Username
        {
            get {                
                return ClaimsPrincipal?.Identity.Name;
            }        
        }

        public int TenantId
        {
            get
            {
                return Convert.ToInt16(ClaimsPrincipal?.Claims.Single(x => x.Type == Security.ClaimTypes.TenantId).Value);
            }
        }
    }
}