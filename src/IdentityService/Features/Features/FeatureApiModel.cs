using IdentityService.Data.Model;

namespace IdentityService.Features.Features
{
    public class FeatureApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromFeature<TModel>(Feature feature) where
            TModel : FeatureApiModel, new()
        {
            var model = new TModel();
            model.Id = feature.Id;
            model.Name = feature.Name;
            return model;
        }

        public static FeatureApiModel FromFeature(Feature feature)
            => FromFeature<FeatureApiModel>(feature);

    }
}
