using Newtonsoft.Json.Linq;

namespace IdentityService.Features.Core
{
    public interface IEventBusMessageHandler
    {
        void Handle(JObject message);
    }
}