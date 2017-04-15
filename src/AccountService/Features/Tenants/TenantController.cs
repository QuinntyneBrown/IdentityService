using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using static AccountService.Features.Tenants.AddOrUpdateTenantCommand;
using static AccountService.Features.Tenants.GetTenantsQuery;
using static AccountService.Features.Tenants.GetTenantByIdQuery;
using static AccountService.Features.Tenants.RemoveTenantCommand;

namespace AccountService.Features.Tenants
{
    [Authorize]
    [RoutePrefix("api/tenant")]
    public class TenantController : ApiController
    {
        public TenantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateTenantResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateTenantRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateTenantResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateTenantRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetTenantsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetTenantsRequest();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetTenantByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetTenantByIdRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveTenantResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveTenantRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
