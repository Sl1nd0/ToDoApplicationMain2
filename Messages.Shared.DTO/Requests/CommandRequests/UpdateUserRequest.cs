using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class UpdateUserRequest : ICommandModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
