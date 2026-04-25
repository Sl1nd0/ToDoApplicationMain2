using Messages.Shared.DTO.Requests.CommandRequests;
using Services.Core.Interfaces;
using Services.Interfaces;
using Messages.Domain.Entities;
using Services.Core.DTO;

namespace Messages.Domain.CommandHandlers
{
    public class AddMessageHandler : ICommandHandler<AddMessageRequest>
    {
        private readonly IRepository<Message> _repository;

        public AddMessageHandler(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public async Task<GenericCommandResult> Execute(AddMessageRequest model)
        {
            var entity = new Message();

            entity.Text = model.Text;
            entity.DateEntered = DateTime.Now;

            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            var result = new GenericCommandResult();
            result.Success = true;

            return result;
        }
    }
}
