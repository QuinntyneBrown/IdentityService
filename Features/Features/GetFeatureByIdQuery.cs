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
    public class GetFeatureByIdQuery
    {
        public class Request : IRequest<Response> { 
            public int Id { get; set; }
        }

        public class Response
        {
            public FeatureApiModel Feature { get; set; } 
        }

        public class GetFeatureByIdHandler : IAsyncRequestHandler<Request, Response>
        {
            public GetFeatureByIdHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {                
                return new Response()
                {
                    Feature = FeatureApiModel.FromFeature(await _context.Features			
					.SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
