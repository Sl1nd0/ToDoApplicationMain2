
namespace Messages.Shared.DTO.Results.QueryResults
{
    public class GetTodoResult
    { 
        public int ToDoID { get; set; }
        public string ToDo { get; set; }
        public string ToDoTitle { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
