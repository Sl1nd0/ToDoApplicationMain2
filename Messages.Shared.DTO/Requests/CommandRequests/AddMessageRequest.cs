using Services.Core.DTO;
using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class AddMessageRequest: ICommandModel
    {
        public string Text { get; set; }
    }
}
