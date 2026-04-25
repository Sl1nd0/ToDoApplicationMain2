using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class UpdateUserRoleHandler : ICommandHandler<UpdateUserRoleRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<UserRole> _repository;

        public UpdateUserRoleHandler(IMessagesDomain context, IRepository<UserRole> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(UpdateUserRoleRequest model)
        {
            var entity = _context.UserRoles.Where(w => w.Id.Equals(model.Id)).FirstOrDefault();

            entity.Role = model.RoleName;
            entity.DateUpdated = DateTime.Now;
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
