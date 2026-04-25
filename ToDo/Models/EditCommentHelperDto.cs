using Messages.Shared.DTO.Results.QueryResults;

namespace ToDo.Models
{
    public class EditCommentHelperDto
    {
        public string Email { get;  set; }
        public string UserName { get;  set; }
        public int UserID { get; internal set; }
        public string ToDo { get; internal set; }
        public int ToDoId { get; internal set; }
        public string ToDoTitle { get; set; }
        public string Comment { get; set; }
        public int CommentID { get;  set; }
        public string Password { get; set; }
        public string CommentCleaned { get; internal set; }
    }
}
