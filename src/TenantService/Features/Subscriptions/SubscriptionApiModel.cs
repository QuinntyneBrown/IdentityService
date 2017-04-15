using TenantService.Data.Model;

namespace TenantService.Features.Subscriptions
{
    public class SubscriptionApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromSubscription<TModel>(Subscription subscription) where
            TModel : SubscriptionApiModel, new()
        {
            var model = new TModel();
            model.Id = subscription.Id;
            model.TenantId = subscription.TenantId;
            model.Name = subscription.Name;
            return model;
        }

        public static SubscriptionApiModel FromSubscription(Subscription subscription)
            => FromSubscription<SubscriptionApiModel>(subscription);

    }
}
