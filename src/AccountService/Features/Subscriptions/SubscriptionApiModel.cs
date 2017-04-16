using AccountService.Data.Model;
using System;

namespace AccountService.Features.Subscriptions
{
    public class SubscriptionApiModel
    {        
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiresOn { get; set; }

        public int? AccountId { get; set; }

        public int? FeatureId { get; set; }

        public static TModel FromSubscription<TModel>(Subscription subscription) where
            TModel : SubscriptionApiModel, new()
        {
            var model = new TModel();

            model.Id = subscription.Id;

            model.Name = subscription.Name;

            model.EffectiveDate = subscription.EffectiveDate;

            model.ExpiresOn = subscription.ExpiresOn;

            model.AccountId = subscription.AccountId;

            model.FeatureId = subscription.FeatureId;

            return model;
        }

        public static SubscriptionApiModel FromSubscription(Subscription subscription)
            => FromSubscription<SubscriptionApiModel>(subscription);

    }
}
