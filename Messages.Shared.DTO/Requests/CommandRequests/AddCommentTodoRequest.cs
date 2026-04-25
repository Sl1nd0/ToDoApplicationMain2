using Services.Core.Interfaces;

namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class AddCommentTodoRequest: ICommandModel
    {
        public int ToDoID { get; set; }
        public int userID { get; set; }
        public string Comment { get; set; }
        public string? UserName { get; set; }
        public string? ToDo { get; set; }
        public string? ToDoTitle { get; set; }
    }
}
