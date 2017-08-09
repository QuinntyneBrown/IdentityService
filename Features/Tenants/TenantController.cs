using IdentityService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IdentityService.Features.Tenants
{
    [Authorize]
    [RoutePrefix("api/tenants")]
    public class TenantController : BaseApiController
    {
        public TenantController(IMediator mediator)
            :base(mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateTenantCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateTenantCommand.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateTenantCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateTenantCommand.Request request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTenantsQuery.Response))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTenantsQuery.Request();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetTenantByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetTenantByIdQuery.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveTenantCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveTenantCommand.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
