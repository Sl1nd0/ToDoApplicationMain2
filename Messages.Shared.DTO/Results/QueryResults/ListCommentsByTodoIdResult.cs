namespace Messages.Shared.DTO.Results.QueryResults
{
    public class ListCommentsByTodoIdResult
    {
        public string Comment { get; set; }
        public string CommentCleaned { get; set; }
        public DateTime CommentDate { get; set; }
        public string UserName { get; set; }
        public string Error { get; set; } 
        public int commentID { get; set; } 
        public int UserID { get; set; } 
    }
}
