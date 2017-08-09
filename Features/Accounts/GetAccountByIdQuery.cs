using IdentityService.Data;
using IdentityService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IdentityService.Features.Accounts
{
    public class GetAccountByIdQuery
    {
        public class Request : BaseRequest, IRequest<Response> { 
            public int Id { get; set; }
        }

        public class Response
        {
            public AccountApiModel Account { get; set; } 
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {                
                return new Response()
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
