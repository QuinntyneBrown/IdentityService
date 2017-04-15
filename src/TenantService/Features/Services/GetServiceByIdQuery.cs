using MediatR;
using TenantService.Data;
using TenantService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TenantService.Features.Services
{
    public class GetServiceByIdQuery
    {
        public class GetServiceByIdRequest : IRequest<GetServiceByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetServiceByIdResponse
        {
            public ServiceApiModel Service { get; set; } 
        }

        public class GetServiceByIdHandler : IAsyncRequestHandler<GetServiceByIdRequest, GetServiceByIdResponse>
        {
            public GetServiceByIdHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetServiceByIdResponse> Handle(GetServiceByIdRequest request)
            {                
                return new GetServiceByIdResponse()
                {
                    Service = ServiceApiModel.FromService(await _context.Services		
					.SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
