using MediatR;
using IdentityService.Data;
using IdentityService.Model;
using IdentityService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace IdentityService.Features.Features
{
    public class RemoveFeatureCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response { }

        public class RemoveFeatureHandler : IAsyncRequestHandler<Request, Response>
        {
            public RemoveFeatureHandler(IdentityServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var feature = await _context.Features.SingleAsync(x=>x.Id == request.Id);
                feature.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IdentityServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
