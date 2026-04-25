using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class AddTodosRequest: ICommandModel
    {
        public int UsersID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ToDo { get; set; }
        public string Title { get; set; }
        public int ToDoID { get; set; }
    }
}
