using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class DeleteTodosHandler : ICommandHandler<DeleteTodosRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<ToDo> _repository;

        public DeleteTodosHandler(IMessagesDomain context,
            IRepository<ToDo> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(DeleteTodosRequest model)
        {
            var addTodoResult = new GenericCommandResult();

            try
            {
                var entity = await _repository.FindByIdAsync(model.ToDoID);

                entity.DateUpdated = DateTime.Now;
                entity.DateDeleted = DateTime.Now;

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
