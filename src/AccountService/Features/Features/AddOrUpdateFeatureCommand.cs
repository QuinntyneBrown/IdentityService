using AccountService.Data;
using AccountService.Data.Model;
using AccountService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AccountService.Features.Features
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
            public AddOrUpdateFeatureHandler(AccountServiceContext context, ICache cache)
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
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateFeatureResponse();
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }
    }
}