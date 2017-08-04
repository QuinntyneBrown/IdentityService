using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Features
{
    public class GetFeaturesQuery
    {
        public class Request : IRequest<Response> { 
     
        }

        public class Response
        {
            public ICollection<FeatureApiModel> Features { get; set; } = new HashSet<FeatureApiModel>();
        }

        public class GetFeaturesHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetFeaturesHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var features = await _context.Features
                    .ToListAsync();

                return new Response()
                {
                    Features = features.Select(x => FeatureApiModel.FromFeature(x)).ToList()
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
