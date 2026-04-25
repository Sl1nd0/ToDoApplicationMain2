using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class AddUserRoleRequest: ICommandModel
    {
        public string RoleName { get; set; }
    }
}
