using IdentityService.Features.DigitalAssets.UploadHandlers;
using IdentityService.Security;
using MediatR;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http.Headers;
using IdentityService.Features.Core;

namespace IdentityService.Features.DigitalAssets
{
    [Authorize]
    [RoutePrefix("api/digitalassets")]
    public class DigitalAssetController : BaseApiController
    {        
        public DigitalAssetController(IMediator mediator, IUserManager userManager)
            :base(mediator)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateDigitalAssetCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateDigitalAssetCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetDigitalAssetsQuery.Response))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetDigitalAssetsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetDigitalAssetByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetDigitalAssetByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveDigitalAssetCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("serve")]
        [HttpGet]
        [ResponseType(typeof(GetDigitalAssetByUniqueIdQuery.Response))]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Serve([FromUri]GetDigitalAssetByUniqueIdQuery.Request request)
        {
            var response = await _mediator.Send(request);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(response.DigitalAsset.Bytes);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(response.DigitalAsset.ContentType);
            return result;
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IHttpActionResult> Upload(HttpRequestMessage request)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var user = await _userManager.GetUserAsync(User);            
            var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());            
            return Ok(await _mediator.Send(new AzureBlobStorageDigitalAssetCommand.AzureBlobStorageDigitalAssetRequest() { Provider = provider, Folder = $"{user.Tenant.UniqueId}" }));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}