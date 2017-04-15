using MediatR;
using TenantService.Data;
using TenantService.Data.Model;
using TenantService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace TenantService.Features.Tenants
{
    public class AddOrUpdateTenantCommand
    {
        public class AddOrUpdateTenantRequest : IRequest<AddOrUpdateTenantResponse>
        {
            public TenantApiModel Tenant { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateTenantResponse { }

        public class AddOrUpdateTenantHandler : IAsyncRequestHandler<AddOrUpdateTenantRequest, AddOrUpdateTenantResponse>
        {
            public AddOrUpdateTenantHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateTenantResponse> Handle(AddOrUpdateTenantRequest request)
            {
                var entity = await _context.Tenants                    
                    .SingleOrDefaultAsync(x => x.Id == request.Tenant.Id);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Tenants.Add(entity = new Tenant() { });
                }

                entity.Name = request.Tenant.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateTenantResponse();
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
