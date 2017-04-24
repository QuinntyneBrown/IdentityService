using MediatR;
using IdentityService.Data;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Accounts
{
    public class GetAccountByIdQuery
    {
        public class GetAccountByIdRequest : IRequest<GetAccountByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetAccountByIdResponse
        {
            public AccountApiModel Account { get; set; } 
        }

        public class GetAccountByIdHandler : IAsyncRequestHandler<GetAccountByIdRequest, GetAccountByIdResponse>
        {
            public GetAccountByIdHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetAccountByIdResponse> Handle(GetAccountByIdRequest request)
            {                
                return new GetAccountByIdResponse()
                {
                    Account = AccountApiModel.FromAccount(await _context.Accounts
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
