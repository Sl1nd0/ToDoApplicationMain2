using Messages.Shared.DTO.Requests.QueryRequests;
using Microsoft.EntityFrameworkCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class ListMessagesQuery : IQuery<ListMessagesRequest, List<ListMessagesResult>>
    {
        private readonly IMessagesDomain _context;

        public ListMessagesQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<List<ListMessagesResult>> Query(ListMessagesRequest model)
        {
            var result = new List<ListMessagesResult>();

            var query = await _context.Messages.Select(w => new ListMessagesResult
            {
                Id = w.Id,
                Message = w.Text,
                DateEntered = w.DateEntered,
                DateUpdated = w.DateUpdated,
                DateDeleted = w.DateDeleted,
            }).ToListAsync();

            return query;
        }
    }
}
