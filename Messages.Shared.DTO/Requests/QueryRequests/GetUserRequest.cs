namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class GetUserRequest
    {
        public bool TasksView { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public int ConnectionID { get; set; }
        public string TaskEmail { get; set; }
        public int ToDoID { get; set; }
        public string ToDo { get; set; }
    }
}
