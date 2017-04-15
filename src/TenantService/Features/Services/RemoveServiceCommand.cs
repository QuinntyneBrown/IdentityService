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
    public class RemoveServiceCommand
    {
        public class RemoveServiceRequest : IRequest<RemoveServiceResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveServiceResponse { }

        public class RemoveServiceHandler : IAsyncRequestHandler<RemoveServiceRequest, RemoveServiceResponse>
        {
            public RemoveServiceHandler(TenantServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveServiceResponse> Handle(RemoveServiceRequest request)
            {
                var service = await _context.Services.SingleAsync(x=>x.Id == request.Id);
                service.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveServiceResponse();
            }

            private readonly TenantServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
