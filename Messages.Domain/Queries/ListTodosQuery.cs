using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.DomainCore;
using Services.Core.Interfaces;

namespace Messages.Domain.Queries
{
    public class ListTodosQuery : IQuery<ListTodosRequest, List<ListTodosResult>>
    {
        private readonly IMessagesDomain _context;

        public ListTodosQuery(IMessagesDomain context)
        {
            _context = context;
        }

        public async Task<List<ListTodosResult>> Query(ListTodosRequest model)
        {
            var listresult = new List<ListTodosResult>();
            var result = new ListTodosResult();

            if (model.Page == 0 && model.PageSize == 0)
            {
                result.Error = "Page and PageSize must be greater than 0";
                listresult.Add(result);

                return listresult;
            }

            var min = DateTime.MinValue;

            var queryResult = await _context.ToDos.ActiveRows()
                .OrderByDescending(o=> o.DateEntered)
                .Where(s => (s.UsersID.Equals(model.UserID)
                        && (string.IsNullOrEmpty(model.Search)
                        || s._ToDo.ToUpper().Contains(model.Search.ToUpper())))
                        && s.DateDeleted.Equals(DateTime.MinValue))       
                .Select(s => new ListTodosResult
                {
                    DateEntered = s.DateEntered,
                    _ToDo = s._ToDo,
                    ToDoTitle = s.ToDoTitle,
                    todoID = s.Id,
                    UsersID = s.UsersID
                })
                .ToListAsync();

            if (!queryResult.Any())
            {
                result.Error = "Query returned no results";
                return listresult;
            }

            listresult.AddRange(queryResult);

            return listresult;
        }         
    }
}
