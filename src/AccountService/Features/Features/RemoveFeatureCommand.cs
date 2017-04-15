using MediatR;
using AccountService.Data;
using AccountService.Data.Model;
using AccountService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace AccountService.Features.Features
{
    public class RemoveFeatureCommand
    {
        public class RemoveFeatureRequest : IRequest<RemoveFeatureResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveFeatureResponse { }

        public class RemoveFeatureHandler : IAsyncRequestHandler<RemoveFeatureRequest, RemoveFeatureResponse>
        {
            public RemoveFeatureHandler(AccountServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveFeatureResponse> Handle(RemoveFeatureRequest request)
            {
                var feature = await _context.Features.SingleAsync(x=>x.Id == request.Id);
                feature.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveFeatureResponse();
            }

            private readonly AccountServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
