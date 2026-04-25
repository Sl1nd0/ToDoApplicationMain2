using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Domain.Queries
{
    public class ListTodosByUsernameQuery : IQuery<ListTodosByUsernameRequest, List<ListTodosByUsernameResult>>
    {
        private readonly IMessagesDomain _context;

        public ListTodosByUsernameQuery(IMessagesDomain context)
        {
            _context = context;
        }


        public async Task<List<ListTodosByUsernameResult>> Query(ListTodosByUsernameRequest model)
        {
            var listTodosByUsernameResult = new List<ListTodosByUsernameResult>();
            var min = DateTime.MinValue;

            var query = await _context.ToDos.Where(w => w.DateDeleted <= min && 
            w.UsersID.Equals(model.UserID))
                .Select(s => new ListTodosByUsernameResult
                {
                    UsersID = s.UsersID,
                    ToDo = s._ToDo,
                    ToDoTitle = s.ToDoTitle,
                    Id = s.Id,
                })
                .ToListAsync();

            if (query.Count() == 0)
            {
                return query;
            }

            listTodosByUsernameResult.AddRange(query);

            return listTodosByUsernameResult;
        }
    }
}
