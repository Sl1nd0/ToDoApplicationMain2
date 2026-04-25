using Messages.Shared.DTO.Results.QueryResults;

namespace ToDo.Models
{
    public class CommentUserHelperDto
    {
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
        public int UserID { get; internal set; }
        public string ToDo { get; internal set; }
        public int ToDoID { get; internal set; }
        public string ToDoTitle { get; internal set; }
        public List<ListCommentsByTodoIdResult>? ListCommentsByTodoIdResult { get; set; }
        public string UserTasks { get; internal set; }
    }
}
