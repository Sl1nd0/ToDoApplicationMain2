using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Services.Core.DomainCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class CountCommentsByToDoIdQuery : IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult>
    {
        private readonly IMessagesDomain _context;

        public CountCommentsByToDoIdQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<CountCommentsByToDoIdResult> Query(CountCommentsByToDoIdRequest model)
        {
            var countCommentsByToDoIdResult = new CountCommentsByToDoIdResult();

            try
            {
                var count = _context.Comments.ActiveRows().Where(w => w.ToDoID.Equals(model.ToDoID)).Count();

                countCommentsByToDoIdResult.CommentsCount = count;

                return countCommentsByToDoIdResult;
            }
            catch (Exception ex)
            {
                countCommentsByToDoIdResult.Error = ex.Message;
                return countCommentsByToDoIdResult;
            }
        }
    }
}
