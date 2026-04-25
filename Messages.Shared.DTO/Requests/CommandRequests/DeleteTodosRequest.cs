namespace Messages.Shared.DTO.Requests.CommandRequests
{
    public class DeleteTodosRequest
    {
        public string ToDo { get; set; }
        public string UserName { get; set; }
        public int ToDoID{ get; set; }
        public string? ToDoTitle { get; set; }
    }
}
