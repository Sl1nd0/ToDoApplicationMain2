using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class EditCommentRequest: ICommandModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
