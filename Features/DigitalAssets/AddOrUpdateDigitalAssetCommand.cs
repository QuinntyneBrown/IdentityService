using MediatR;
using IdentityService.Data;
using IdentityService.Model;
using System.Threading.Tasks;
using System.Data.Entity;
using IdentityService.Features.Core;

namespace IdentityService.Features.DigitalAssets
{
    public class AddOrUpdateDigitalAssetCommand
    {
        public class Request : IRequest<Response>
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class Response { }

        public class AddOrUpdateDigitalAssetHandler : IAsyncRequestHandler<Request, Response>
        {
            public AddOrUpdateDigitalAssetHandler(IIdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var entity = await _context.DigitalAssets
                    .SingleOrDefaultAsync(x => x.Id == request.DigitalAsset.Id && x.IsDeleted == false);
                if (entity == null) _context.DigitalAssets.Add(entity = new DigitalAsset());
                entity.Name = request.DigitalAsset.Name;
                entity.Folder = request.DigitalAsset.Folder;
                await _context.SaveChangesAsync();

                return new Response() { };
            }

            private readonly IIdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
