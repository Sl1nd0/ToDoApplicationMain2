using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.EntityFrameworkCore;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class AddTodosHandler : ICommandHandler<AddTodosRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<ToDo> _repository;

        public AddTodosHandler(IMessagesDomain context,
            IRepository<ToDo> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(AddTodosRequest model)
        {
            var addTodoResult = new GenericCommandResult();

            try
            {
                var query = await _context.Users
                        .Where(u => u.Id.Equals(model.UsersID) 
                        //|| u.UserName.ToUpper().Equals(model.UserName.ToUpper())
                        || u.Email.ToUpper().Equals(model.Email.ToUpper()))
                        .Select(u => new GetUserResult
                        {
                            Name = u.Name,
                            Surname = u.Surname,
                            UserName = u.UserName,
                            Email = u.Email,
                            Identifier = u.Identifier,
                            Password = u.Password,
                            CellNumber = u.CellNumber,
                            UserID = u.Id
                        })
                        .FirstOrDefaultAsync();

                var entity = new ToDo();

                entity.ToDoTitle = model.Title;
                entity._ToDo = model.ToDo;
                entity.UsersID = query.UserID;

                entity.DateEntered = DateTime.Now;

                _repository.Add(entity);

                await _repository.SaveChangesAsync();
                addTodoResult.Success = true;

                return addTodoResult;
            }
            catch (Exception ex)
            {
                addTodoResult.Success = false;
                addTodoResult.Errors.Add(ex.Message);
                return addTodoResult;
            }
        }
    }
}
