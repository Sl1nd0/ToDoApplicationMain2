using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class DeleteUserRequest : ICommandModel
    {
        public int Id { get; set; }
    }
}
