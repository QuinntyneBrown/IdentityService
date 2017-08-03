using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IdentityService.Features.Core;

namespace IdentityService.Features.Subscriptions
{
    [Authorize]
    [RoutePrefix("api/subscriptions")]
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
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSubscriptionResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSubscriptionRequest request)
            => Ok(await _mediator.Send(request));

        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSubscriptionsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetSubscriptionsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSubscriptionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSubscriptionByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSubscriptionResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSubscriptionRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;
    }
}
