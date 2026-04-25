using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class DeleteUserRoleHandler : ICommandHandler<DeleteUserRoleRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<UserRole> _repository;

        public DeleteUserRoleHandler(IMessagesDomain context,
            IRepository<UserRole> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(DeleteUserRoleRequest model)
        {
            var entity = _context.UserRoles.Where(w => w.Id.Equals(model.Id)).FirstOrDefault();
            entity.DateDeleted = DateTime.Now;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
