using MediatR;
using TenantService.Data;
using TenantService.Data.Model;
using TenantService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TenantService.Features.Services
{
    public class AddOrUpdateServiceCommand
    {
        public class AddOrUpdateServiceRequest : IRequest<AddOrUpdateServiceResponse>
        {
            public ServiceApiModel Service { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateServiceResponse { }

        public class AddOrUpdateServiceHandler : IAsyncRequestHandler<AddOrUpdateServiceRequest, AddOrUpdateServiceResponse>
        {
            public AddOrUpdateServiceHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateServiceResponse> Handle(AddOrUpdateServiceRequest request)
            {
                var entity = await _context.Services
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Service.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Services.Add(entity = new Service() { TenantId = tenant.Id });
                }

                entity.Name = request.Service.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateServiceResponse();
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
