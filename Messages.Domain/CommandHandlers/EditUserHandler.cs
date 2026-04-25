using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class EditUserHandler : ICommandHandler<EditUserRequest>
    {
        private readonly IRepository<User> _repository;
        private readonly IMessagesDomain _context;

        public EditUserHandler(IRepository<User> repository, IMessagesDomain context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<GenericCommandResult> Execute(EditUserRequest model)
        {
            var result = new GenericCommandResult();

            var entity = await _repository.FindByIdAsync(model.UserId);

            if (entity == null)
            {
                result.Errors.Add($"A user doesn't exist for Id {model.UserId}");
            }

            entity.Name = model.Name;
            entity.Surname = model.Surname;
            entity.UserName = model.UserName;
            entity.Surname = model.Surname;
            entity.Email = model.Email;
            entity.Identifier = model.Identifier;
            entity.DateOfBirth = model.DateOfBirth;
            entity.CellNumber = model.CellNumber;

            entity.DateUpdated = DateTime.Now;

            _repository.Update(entity);

            await _repository.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}
