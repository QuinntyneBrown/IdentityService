using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IdentityService.Features.Core;
using static IdentityService.Features.Features.AddOrUpdateFeatureCommand;
using static IdentityService.Features.Features.GetFeaturesQuery;
using static IdentityService.Features.Features.GetFeatureByIdQuery;
using static IdentityService.Features.Features.RemoveFeatureCommand;

namespace IdentityService.Features.Features
{
    [Authorize]
    [RoutePrefix("api/feature")]
    public class FeatureController : ApiController
    {
        public FeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateFeatureResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateFeatureRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateFeatureResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateFeatureRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetFeaturesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetFeaturesRequest();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetFeatureByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetFeatureByIdRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveFeatureResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveFeatureRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
