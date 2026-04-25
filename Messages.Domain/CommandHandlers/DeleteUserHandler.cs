using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class DeleteUserHandler : ICommandHandler<DeleteUserRequest>
    {
        private readonly IRepository<User> _repository;
        private readonly IMessagesDomain _context;

        public DeleteUserHandler(IRepository<User> repository,
            IMessagesDomain context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<GenericCommandResult> Execute(DeleteUserRequest model)
        {
            var entity = _context.Users.Where(w => w.Id.Equals(model.Id)).FirstOrDefault();
            entity.DateDeleted = DateTime.Now;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
