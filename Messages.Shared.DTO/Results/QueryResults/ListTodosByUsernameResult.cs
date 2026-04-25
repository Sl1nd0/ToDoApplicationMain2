namespace Messages.Shared.DTO.Results.QueryResults
{
    public class ListTodosByUsernameResult
    {
        public int Id { get; set; }
        public int UsersID { get; set; }
        public string ToDo { get; set; }
        public string ToDoTitle { get; set; }
        public string Errors { get; set; }

    }
}
