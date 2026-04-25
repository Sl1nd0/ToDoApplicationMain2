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
    public class AddConnectionHandler : ICommandHandler<AddConnectionRequest>
    {
        private readonly IRepository<Connection> _repository;

        public AddConnectionHandler(IRepository<Connection> repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(AddConnectionRequest model)
        {
            var result = new GenericCommandResult();

            var entity = await _repository.FindBySurrogateAsync(f => f.UserID == model.UserID && f.ConnectionUserID == model.ConnectionUserID);

            result.Success = false;

            if (entity != null)
            {
                result.Errors.Add($"entity already exists for the combo userID {model.UserID} connection {model.ConnectionUserID}");
                return result;
            }

            entity = await _repository.FindBySurrogateAsync(f => f.UserID == model.ConnectionUserID && f.ConnectionUserID == model.UserID);

            result.Success = false;
            if (entity != null)
            {
                result.Errors.Add($"entity already exists for the combo userID {model.UserID} connection {model.ConnectionUserID}");
                return result;
            }

            entity = new Connection();
            entity.ConnectionUserID = model.ConnectionUserID;
            entity.UserID = model.UserID;

            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}
