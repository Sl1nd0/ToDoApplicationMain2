using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class DeleteCommentRequest: ICommandModel
    {
        public int Id { get; set; }
    }
}
