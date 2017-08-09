using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IdentityService.Features.Core;

namespace IdentityService.Features.Features
{
    [Authorize]
    [RoutePrefix("api/features")]
    public class FeaturesController : BaseApiController
    {
        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateFeatureCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateFeatureCommand.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateFeatureCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateFeatureCommand.Request request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetFeaturesQuery.Response))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetFeaturesQuery.Request();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetFeatureByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetFeatureByIdQuery.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveFeatureCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveFeatureCommand.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
