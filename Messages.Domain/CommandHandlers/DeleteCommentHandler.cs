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
    public class DeleteCommentHandler : ICommandHandler<DeleteCommentRequest>
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMessagesDomain _context;

        public DeleteCommentHandler(IRepository<Comment> repository, 
            IMessagesDomain context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<GenericCommandResult> Execute(DeleteCommentRequest model)
        {
            var result = new GenericCommandResult();

            var entity = await _repository.FindByIdAsync(model.Id);

            if (entity == null)
            {
                result.Success = false;
                result.Errors.Add($"Comment doesn't exist for Id {model.Id}");
                return result;
            }

            entity.DateUpdated = DateTime.Now;
            entity.DateDeleted = DateTime.Now;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}
