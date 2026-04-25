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
    public class DeleteUserConnectionHandler : ICommandHandler<DeleteUserConnectionRequest>
    {
        private readonly IRepository<Connection> _repository;
        private readonly IMessagesDomain _context;

        public DeleteUserConnectionHandler(IRepository<Connection> repository, IMessagesDomain context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<GenericCommandResult> Execute(DeleteUserConnectionRequest model)
        {
            var result = new GenericCommandResult();

            var entity = await _repository.FindByIdAsync(model.Id);

            _repository.Delete(entity);

            await _repository.SaveChangesAsync();

            result.Success = true;

            //entity = await _repository.FindByIdAsync(model.Id);

            return result;
        }
    }
}
