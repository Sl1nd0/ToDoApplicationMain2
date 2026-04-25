namespace Messages.Shared.DTO.Results.QueryResults
{
    public class ListTodosResult
    {
        public int todoID { get; set; }
        public int UsersID { get; set; }
        public string _ToDo { get; set; }
        public string ToDoTitle { get; set; }
        public string Error { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateUpdates { get; set; }
        public int CommentsCount { get; set; }
    }
}
