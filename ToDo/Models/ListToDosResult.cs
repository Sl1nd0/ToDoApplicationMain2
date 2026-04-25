using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;

namespace ToDo.Models
{
    public class ListToDosResult
    {
        public List<ListTodosResult> ListTodosResult { get; set; } = new();
        public GetUserRequest? GetUserRequest { get; set; }
        public GetUserDetailsResult? GetUserDetailsResult { get; set; }
        public EditUserHelperDto? EditUserHelperDto { get; set; }
        public DeleteUserHelperDto? DeleteUserHelperDto { get; set; }
        public CommentUserHelperDto? CommentUserHelperDto { get; set; }
        public EditCommentHelperDto? EditCommentHelperDto { get; set; }
        public List<ListPossibleConnectionsResult>? ListPossibleConnectionsResult { get; set; } = new();
        public List<ListConnectionsByUserIdResult>? ListConnectionsByUserIdResult { get; set; } = new();
    }
}
