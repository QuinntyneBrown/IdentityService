using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Features
{
    public class AddOrUpdateFeatureCommand
    {
        public class AddOrUpdateFeatureRequest : IRequest<AddOrUpdateFeatureResponse>
        {
            public FeatureApiModel Feature { get; set; }
        }

        public class AddOrUpdateFeatureResponse { }

        public class AddOrUpdateFeatureHandler : IAsyncRequestHandler<AddOrUpdateFeatureRequest, AddOrUpdateFeatureResponse>
        {
            public AddOrUpdateFeatureHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateFeatureResponse> Handle(AddOrUpdateFeatureRequest request)
            {
                var entity = await _context.Features
                    .SingleOrDefaultAsync(x => x.Id == request.Feature.Id);
                
                if (entity == null) {
                    _context.Features.Add(entity = new Feature() { });
                }

                entity.Name = request.Feature.Name;

                entity.Url = request.Feature.Url;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateFeatureResponse();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}