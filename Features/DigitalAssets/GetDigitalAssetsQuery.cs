using MediatR;
using IdentityService.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using IdentityService.Data.Model;
using static IdentityService.Features.DigitalAssets.Constants;
using IdentityService.Features.Core;

namespace IdentityService.Features.DigitalAssets
{
    public class GetDigitalAssetsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; } = new HashSet<DigitalAssetApiModel>();
        }

        public class GetDigitalAssetsHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetDigitalAssetsHandler(IIdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var digitalAssets = await _cache.FromCacheOrServiceAsync<List<DigitalAsset>>(() => _context.DigitalAssets.ToListAsync(), DigitalAssetCacheKeys.DigitalAssets);

                return new Response()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly IIdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}