using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class AddConnectionRequest: ICommandModel
    {
        public int UserID { get; set; }
        public int ConnectionUserID { get; set; }
    }
}
