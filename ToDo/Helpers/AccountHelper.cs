using Messages.Domain.CommandHandlers;
using Messages.Domain.Queries;
using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Services.Core.Interfaces;
using System.Text;
using ToDo.Models;

namespace ToDo.Helpers
{
    public class AccountHelper
    {
        private readonly ICommandHandler<AddUserRequest> _addUserHandler;
        private readonly IQuery<ListTodosRequest, List<ListTodosResult>> _listTodosQuery;
        private readonly IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult> _countCommentsByToDoIdQuery;
        private readonly IQuery<GetUserRequest, GetUserResult> _getUserQuery;

        public AccountHelper(ICommandHandler<AddUserRequest> addUserHandler,
            IQuery<ListTodosRequest, List<ListTodosResult>> listTodosQuery,            
            IQuery<GetUserRequest, GetUserResult> getUserQuery,
            IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult> countCommentsByToDoIdQuery)
        {
            _addUserHandler = addUserHandler;
            _listTodosQuery = listTodosQuery;
            _countCommentsByToDoIdQuery = countCommentsByToDoIdQuery;
            _getUserQuery = getUserQuery;
        }

        public ListToDosResult ListTodosQueryResult(string email, string username, int userID)
        {
            var getUserRequest = new GetUserRequest();

            getUserRequest.Email = email;
            getUserRequest.UserName = username;

            var listTodosQueryResult = _listTodosQuery.Query(new ListTodosRequest
            {
                Page = 1,
                PageSize = 7,
                Search = "",
                UserID = userID
            });
            listTodosQueryResult.Wait();

            var _listTodosQueryResult = listTodosQueryResult.Result;


            for (var i = 0; i < _listTodosQueryResult.Count(); i++)
            {
                var commentsTask = _countCommentsByToDoIdQuery.Query(new CountCommentsByToDoIdRequest { ToDoID = _listTodosQueryResult[i].todoID });
                commentsTask.Wait();

                _listTodosQueryResult[i].CommentsCount = commentsTask.Result.CommentsCount;
            }


        var listToDosResult = new ListToDosResult
        {
            ListTodosResult = _listTodosQueryResult,
            GetUserRequest = getUserRequest
        };

            return listToDosResult;
        }

        //List comment count for the comments 


        public string BtoaEquivalent(string encoded)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(encoded);
            string base64 = Convert.ToBase64String(bytes);

            //decoded
            return base64;
        }

        public string AtobEquivalent(string any)
        {
            byte[] bytes = Convert.FromBase64String(any);

            string encoded = Encoding.GetEncoding(28591).GetString(bytes);

            //encoded
            return encoded;
        }

        public bool checkLoggedIn(string username, string password)
        {
            var loggedIn = _getUserQuery.Query(new GetUserRequest { Email = username, Password = AtobEquivalent(password) });

            loggedIn.Wait();

            if (loggedIn.Result.Email == null)
            {
                return false;
            }

            return true;
        }
    }
}
