using Messages.Shared.DTO.Requests.QueryRequests;
using Microsoft.EntityFrameworkCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class GetMessageQuery : IQuery<GetMessageRequest, GetMessageResult>
    {
        private readonly IMessagesDomain _context;
        private DateTime DateUpdated;

        public GetMessageQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<GetMessageResult> Query(GetMessageRequest model)
        {
            var result = new GetMessageResult();

            var query = await _context.Messages.Where(w => w.Id.Equals(model.Id)).FirstOrDefaultAsync();

            result.Message = query.Text;
            result.DateEntered = query.DateEntered;
            result.DateUpdated = query.DateUpdated;
            result.DateDeleted = query.DateDeleted;
            result.Id = model.Id;

            return result;
        }         
    }
}
