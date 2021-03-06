using IdentityService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IdentityService.Features.Accounts
{
    [Authorize]
    [RoutePrefix("api/accounts")]
    public class AccountController : BaseApiController
    {
        public AccountController(IMediator mediator)
            :base(mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateAccountCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateAccountCommand.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateAccountCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateAccountCommand.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetAccountsQuery.Response))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetAccountsQuery.Request();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetAccountByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetAccountByIdQuery.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveAccountCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveAccountCommand.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
