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
            public RemoveServiceHandler(AccountServiceContext context, ICache cache)
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

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
