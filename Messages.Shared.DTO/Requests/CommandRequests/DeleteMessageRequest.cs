using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class DeleteMessageRequest: ICommandModel
    {
        public int Id { get; set; }
    }
}
