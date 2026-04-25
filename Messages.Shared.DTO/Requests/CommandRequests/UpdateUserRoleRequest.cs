using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class UpdateUserRoleRequest: ICommandModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
