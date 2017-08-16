using IdentityService.Model;

namespace IdentityService.Features.Features
{
    public class FeatureApiModel
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public static TModel FromFeature<TModel>(Feature feature) where
            TModel : FeatureApiModel, new()
        {
            var model = new TModel();

            model.Id = feature.Id;

            model.Name = feature.Name;

            model.Url = feature.Url;

            return model;
        }

        public static FeatureApiModel FromFeature(Feature feature)
            => FromFeature<FeatureApiModel>(feature);

    }
}
