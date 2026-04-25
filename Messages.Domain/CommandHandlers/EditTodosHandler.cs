using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class EditTodosHandler : ICommandHandler<EditTodosRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<ToDo> _repository;

        public EditTodosHandler(IMessagesDomain context,
            IRepository<ToDo> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(EditTodosRequest model)
        {
            var addTodoResult = new GenericCommandResult();

            try
            {
                var entity = await _repository.FindByIdAsync(model.ToDoID);

                entity._ToDo = model.ToDo;
                entity.ToDoTitle = model.ToDoTitle;
                entity.DateUpdated = DateTime.Now;

                _repository.Update(entity);

                await _repository.SaveChangesAsync();

                var result = new GenericCommandResult();
                result.Success = true;

                return result;
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
