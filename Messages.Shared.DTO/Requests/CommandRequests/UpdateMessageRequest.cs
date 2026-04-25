using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class UpdateMessageRequest: ICommandModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
