using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.DomainCore;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.Queries
{
    public class GetTodoQuery : IQuery<GetTodoRequest, GetTodoResult>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<ToDo> _repository;

        public GetTodoQuery(IMessagesDomain context, IRepository<ToDo> repository)
        {
            _context = context;
            _repository = repository;
        }


        public async Task<GetTodoResult> Query(GetTodoRequest model)
        {
            var query = _context.ToDos.ActiveRows();

            var entityt = await _repository.FindByIdAsync(model.Id);

            var result = await query.Where(w => w.ToDoTitle.ToUpper().Contains(model.Search.ToUpper()) ||
                                                w._ToDo.ToUpper().Contains(model.Search.ToUpper()) ||
                                                w.Id.Equals(model.Id)).
                                                Select(s => new GetTodoResult
                                                {
                                                    ToDo = s._ToDo,
                                                    ToDoTitle = s.ToDoTitle,
                                                    ToDoID = s.Id
                                                }).
                                                FirstOrDefaultAsync();


            return result;
        }
    }
}
