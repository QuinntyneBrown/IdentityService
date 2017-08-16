using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Unity.WebApi;
using Microsoft.Practices.Unity;
using IdentityService.Features.Core;
using Microsoft.ServiceBus.Messaging;

using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(IdentityService.Startup))]

namespace IdentityService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                var container = UnityConfiguration.GetContainer();
                config.DependencyResolver = new UnityDependencyResolver(container);
                ApiConfiguration.Install(config, app);

                var client = SubscriptionClient.CreateFromConnectionString(CoreConfiguration.Config.EventQueueConnectionString, CoreConfiguration.Config.TopicName, CoreConfiguration.Config.SubscriptionName);

                var accountsEventBusMessageHandler = container.Resolve<Features.Accounts.IAccountsEventBusMessageHandler>();
                var digitalAssetsEventBusMessageHandler = container.Resolve<Features.DigitalAssets.IDigitalAssetsEventBusMessageHandler>();
                var featuresEventBusMessageHandler = container.Resolve<Features.Features.IFeaturesEventBusMessageHandler>();
                var profilesEventBusMessageHandler = container.Resolve<Features.Profiles.IProfilesEventBusMessageHandler>();
                var subscriptionsEventBusMessageHandler = container.Resolve<Features.Subscriptions.ISubscriptionsEventBusMessageHandler>();
                var tenantsEventBusMessageHandler = container.Resolve<Features.Tenants.ITenantsEventBusMessageHandler>();
                var usersEventBusMessageHandler = container.Resolve<Features.Users.IUsersEventBusMessageHandler>();

                client.OnMessage(message =>
                {
                    try
                    {
                        var messageBody = ((BrokeredMessage)message).GetBody<string>();
                        var messageBodyObject = DeserializeObject<JObject>(messageBody, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                            TypeNameHandling = TypeNameHandling.All,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });

                        accountsEventBusMessageHandler.Handle(messageBodyObject);
                        digitalAssetsEventBusMessageHandler.Handle(messageBodyObject);
                        featuresEventBusMessageHandler.Handle(messageBodyObject);
                        profilesEventBusMessageHandler.Handle(messageBodyObject);
                        subscriptionsEventBusMessageHandler.Handle(messageBodyObject);
                        tenantsEventBusMessageHandler.Handle(messageBodyObject);
                        usersEventBusMessageHandler.Handle(messageBodyObject);
                    }
                    catch (Exception e)
                    {

                    }
                });
            });
        }
    }
}