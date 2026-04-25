namespace Messages.Shared.DTO.Requests.QueryRequests
{
    public class ListCommentsByTodoIdRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int ToDoID { get; set; }
        public string Search { get; set; }

    }
}
