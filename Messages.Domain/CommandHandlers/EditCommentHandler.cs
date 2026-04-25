using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    internal class EditCommentHandler : ICommandHandler<EditCommentRequest>
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMessagesDomain _context;

        public EditCommentHandler(IRepository<Comment> repository, IMessagesDomain context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<GenericCommandResult> Execute(EditCommentRequest model)
        {
            var result = new GenericCommandResult();

            var entity = await _repository.FindByIdAsync(model.Id);

            if (entity == null)
            {
                result.Success = false;
                result.Errors.Add($"Entity doesn't exist for Id {model.Id}");
            }

            entity._Comment = model.Comment;
            entity.DateUpdated = DateTime.Now;

            await _repository.SaveChangesAsync();

            result.Success = true;

            return result;
        }
    }
}
