using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.DTO;
using Services.Core.Interfaces;
using Services.Interfaces;

namespace Messages.Domain.CommandHandlers
{
    public class UpdateMessageHandler : ICommandHandler<UpdateMessageRequest>
    {
        private readonly IMessagesDomain _context;
        private readonly IRepository<Message> _repository;

        public UpdateMessageHandler(IMessagesDomain context, IRepository<Message> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(UpdateMessageRequest model)
        {
            var entity = _context.Messages.Where(w => w.Id.Equals(model.Id)).FirstOrDefault();

            entity.Text = model.Text;
            entity.DateUpdated = DateTime.Now;
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
