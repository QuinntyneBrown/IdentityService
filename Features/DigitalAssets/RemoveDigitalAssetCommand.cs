using MediatR;
using IdentityService.Data;
using System.Threading.Tasks;
using IdentityService.Features.Core;

namespace IdentityService.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response { }

        public class RemoveDigitalAssetHandler : IAsyncRequestHandler<Request, Response>
        {
            public RemoveDigitalAssetHandler(IIdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var digitalAsset = await _context.DigitalAssets.FindAsync(request.Id);
                digitalAsset.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IIdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
