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
    public class AddUserConnectionHandler : ICommandHandler<AddUserConnectionRequest>
    {
        private readonly IRepository<Connection> _repository;

        public AddUserConnectionHandler(IRepository<Connection> repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(AddUserConnectionRequest model)
        {
            var result = new GenericCommandResult();

            var entity = new Connection();
            entity.UserID = model.UserID;

            entity.DateEntered = DateTime.Now;
            
            _repository.Add(entity);

            await _repository.SaveChangesAsync();

            result.Success = false;
            
            return result;
        }
    }
}
