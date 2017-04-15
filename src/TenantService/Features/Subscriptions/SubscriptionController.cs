using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TenantService.Features.Core;
using static TenantService.Features.Subscriptions.AddOrUpdateSubscriptionCommand;
using static TenantService.Features.Subscriptions.GetSubscriptionsQuery;
using static TenantService.Features.Subscriptions.GetSubscriptionByIdQuery;
using static TenantService.Features.Subscriptions.RemoveSubscriptionCommand;

namespace TenantService.Features.Subscriptions
{
    [Authorize]
    [RoutePrefix("api/subscription")]
    public class SubscriptionController : ApiController
    {
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateSubscriptionResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateSubscriptionRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSubscriptionResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSubscriptionRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSubscriptionsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetSubscriptionsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSubscriptionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSubscriptionByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSubscriptionResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSubscriptionRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
