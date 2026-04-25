using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class DeleteMessageHandler : ICommandHandler<DeleteMessageRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<Message> _repository;

        public DeleteMessageHandler(IMessagesDomain context,
            IRepository<Message> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(DeleteMessageRequest model)
        {
            var entity = _context.Messages.Where(w => w.Id.Equals(model.Id)).FirstOrDefault();
            entity.DateDeleted = DateTime.Now;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
