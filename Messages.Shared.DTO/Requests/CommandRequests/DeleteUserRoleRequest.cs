using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class DeleteUserRoleRequest: ICommandModel
    {
        public int Id { get; set; }
    }
}
