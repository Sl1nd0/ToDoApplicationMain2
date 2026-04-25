using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Domain.CommandHandlers
{
    public class AddUserRoleHandler : ICommandHandler<AddUserRoleRequest>
    {
        private readonly IRepository<UserRole> _repository;

        public AddUserRoleHandler(IRepository<UserRole> repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(AddUserRoleRequest model)
        {
            var entity = new UserRole();
            //Check if role already exists

            entity.Role = model.RoleName;
            entity.DateEntered = DateTime.Now;

            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
