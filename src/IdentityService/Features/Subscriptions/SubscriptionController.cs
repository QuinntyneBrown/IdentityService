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
    public class SubscriptionController : BaseApiController
    {
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateSubscriptionCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateSubscriptionCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSubscriptionCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSubscriptionCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSubscriptionsQuery.Response))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetSubscriptionsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSubscriptionByIdQuery.GetSubscriptionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSubscriptionByIdQuery.GetSubscriptionByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSubscriptionCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSubscriptionCommand.Request request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;
    }
}
