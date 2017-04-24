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
        public class GetFeatureByIdRequest : IRequest<GetFeatureByIdResponse> { 
            public int Id { get; set; }
        }

        public class GetFeatureByIdResponse
        {
            public FeatureApiModel Feature { get; set; } 
        }

        public class GetFeatureByIdHandler : IAsyncRequestHandler<GetFeatureByIdRequest, GetFeatureByIdResponse>
        {
            public GetFeatureByIdHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetFeatureByIdResponse> Handle(GetFeatureByIdRequest request)
            {                
                return new GetFeatureByIdResponse()
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
