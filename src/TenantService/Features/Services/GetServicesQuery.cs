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
    public class GetServicesQuery
    {
        public class GetServicesRequest : IRequest<GetServicesResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetServicesResponse
        {
            public ICollection<ServiceApiModel> Services { get; set; } = new HashSet<ServiceApiModel>();
        }

        public class GetServicesHandler : IAsyncRequestHandler<GetServicesRequest, GetServicesResponse>
        {
            public GetServicesHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetServicesResponse> Handle(GetServicesRequest request)
            {
                var services = await _context.Services
                    .ToListAsync();

                return new GetServicesResponse()
                {
                    Services = services.Select(x => ServiceApiModel.FromService(x)).ToList()
                };
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
