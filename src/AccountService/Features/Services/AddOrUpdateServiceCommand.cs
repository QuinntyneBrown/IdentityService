using MediatR;
using AccountService.Data;
using AccountService.Data.Model;
using AccountService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace AccountService.Features.Services
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
            public AddOrUpdateServiceHandler(AccountServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateServiceResponse> Handle(AddOrUpdateServiceRequest request)
            {
                var entity = await _context.Services
                    .SingleOrDefaultAsync(x => x.Id == request.Service.Id);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Services.Add(entity = new Service() { });
                }

                entity.Name = request.Service.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateServiceResponse();
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
