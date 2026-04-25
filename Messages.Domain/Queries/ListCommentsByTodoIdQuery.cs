using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.DomainCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class ListCommentsByTodoIdQuery : IQuery<ListCommentsByTodoIdRequest, List<ListCommentsByTodoIdResult>>
    {
        private readonly IMessagesDomain _context;

        public ListCommentsByTodoIdQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<List<ListCommentsByTodoIdResult>> Query(ListCommentsByTodoIdRequest model)
        {
            var result = new List<ListCommentsByTodoIdResult>();

            var min = DateTime.MinValue;

            try
            {
                var query = await _context.Comments.ActiveRows()
                    .OrderByDescending(o=> o.DateEntered)
                    .Where(w => w.DateDeleted <= min  && w.ToDoID.Equals(model.ToDoID))
                    .Join(_context.Users.ActiveRows(), c => c.UserID, u => u.Id,
                                                  (c, u) => new ListCommentsByTodoIdResult
                                                  {
                                                      UserName = u.Email,
                                                      Comment = c._Comment,
                                                      CommentDate = c.DateEntered,
                                                      commentID = c.Id,
                                                      UserID = c.UserID
                                                  }).
                                                  ToListAsync();

                result = query;

                return result;
            }
            catch (Exception ex)
            {
                result.First().Error = ex.Message;

                return result;
            }
        }
    }
}
