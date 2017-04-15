using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TenantService.Features.Core;
using static TenantService.Features.Services.AddOrUpdateServiceCommand;
using static TenantService.Features.Services.GetServicesQuery;
using static TenantService.Features.Services.GetServiceByIdQuery;
using static TenantService.Features.Services.RemoveServiceCommand;

namespace TenantService.Features.Services
{
    [Authorize]
    [RoutePrefix("api/service")]
    public class ServiceController : ApiController
    {
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateServiceResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateServiceResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetServicesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetServicesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetServiceByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetServiceByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveServiceResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
