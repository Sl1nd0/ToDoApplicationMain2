using Messages.Domain.Entities;
using Messages.Domain.Queries;
using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using NuGet.Protocol;
using Services.Core.Interfaces;
using System.Text;
using ToDo.Helpers;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICommandHandler<AddUserRequest> _addUserHandler;
        private readonly IQuery<ListTodosRequest, List<ListTodosResult>> _listTodosQuery;
        private readonly IQuery<GetUserRequest, GetUserResult> _getUserQuery;
        private readonly IQuery<GetUserDetailsRequest, GetUserDetailsResult> _getUserDetailsQuery;
        private readonly ICommandHandler<EditUserRequest> _editUserHandler;
        private readonly IQuery<ListPossibleConnectionsRequest, List<ListPossibleConnectionsResult>> _listPossibleConnectionsQuery;
        private readonly ICommandHandler<AddConnectionRequest> _addConnectionHandler;
        private readonly IQuery<ListConnectionsByUserIdRequest, List<ListConnectionsByUserIdResult>> _listConnectionsByUserIdQuery;
        private readonly ICommandHandler<DeleteUserConnectionRequest> _deleteUserConnectionHandler;
        private readonly IQuery<ListTodosByUsernameRequest, List<ListTodosByUsernameResult>> _listTodosByUsernameQuery;
        private readonly IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult> _countCommentsByToDoIdQuery;

        public AccountController(ICommandHandler<AddUserRequest> addUserHandler,
            IQuery<ListTodosRequest, List<ListTodosResult>> listTodosQuery,
            IQuery<GetUserRequest, GetUserResult> getUserQuery,
            IQuery<GetUserDetailsRequest, GetUserDetailsResult> getUserDetailsQuery,
            ICommandHandler<EditUserRequest> editUserHandler,
            IQuery<ListPossibleConnectionsRequest, List<ListPossibleConnectionsResult>> listPossibleConnectionsQuery,
            ICommandHandler<AddConnectionRequest> addConnectionHandler,
            IQuery<ListConnectionsByUserIdRequest, List<ListConnectionsByUserIdResult>> listConnectionsByUserIdQuery,
            ICommandHandler<DeleteUserConnectionRequest> deleteUserConnectionHandler,
            IQuery<ListTodosByUsernameRequest, List<ListTodosByUsernameResult>> listTodosByUsernameQuery,
            IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult> countCommentsByToDoIdQuery)
        {
            _addUserHandler = addUserHandler;
            _listTodosQuery = listTodosQuery;
            _getUserQuery = getUserQuery;
            _getUserDetailsQuery = getUserDetailsQuery;
            _editUserHandler = editUserHandler;
            _listPossibleConnectionsQuery = listPossibleConnectionsQuery;
            _addConnectionHandler = addConnectionHandler;
            _listConnectionsByUserIdQuery = listConnectionsByUserIdQuery;
            _deleteUserConnectionHandler = deleteUserConnectionHandler;
            _listTodosByUsernameQuery = listTodosByUsernameQuery;
            _countCommentsByToDoIdQuery = countCommentsByToDoIdQuery;
        }

        [HttpPost("/Account/DeleteConnection")]
        public async Task<ActionResult> DeleteConnection([FromBody] DeleteUserConnectionRequest model)
        {
            var deleteUserConnectionResult = await _deleteUserConnectionHandler.Execute(model);

            return Ok(deleteUserConnectionResult);
        }

        [HttpPost("/Account/DownloadReport")]
        public async Task<IActionResult> DownloadReport([FromBody] ListTodosByUsernameRequest model)
        {
            var user = await _getUserDetailsQuery.Query(new GetUserDetailsRequest { UserName = model.UserName, Email = model.UserName });

            var listTodosByUsernameQueryResult = await _listTodosByUsernameQuery.Query(new ListTodosByUsernameRequest
            {
                UserID = user.UserID
            });

            return Ok(listTodosByUsernameQueryResult);
        }

        [HttpGet("/Account/ViewConnections")]
        public async Task<IActionResult> ViewConnections(string userName, string token, string path)
        {

           var user = await _getUserDetailsQuery.Query(new GetUserDetailsRequest { UserName = userName, Email = userName });

            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            //Add essentials
            //Logged in?
            var loggedIn = accountHelper.checkLoggedIn(userName, token);
            if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

            var listToDosResult = new ListToDosResult();
            listToDosResult.GetUserRequest = new GetUserRequest();
            //List comment count for the comments 


            //Set a list of connections

            var listConnectionsByUserIdQueryResult = await _listConnectionsByUserIdQuery.Query(new ListConnectionsByUserIdRequest { UserID = user.UserID });
            //var listConnectionsByUserIdQueryResult = await _listConnectionsByUserIdQuery.Query(new ListConnectionsByUserIdRequest { UserID = 1 });

            listToDosResult.ListConnectionsByUserIdResult = listConnectionsByUserIdQueryResult;
            //Set a list of connections

            listToDosResult.GetUserDetailsResult = user;
            listToDosResult.GetUserRequest.Email = user.Email;
            listToDosResult.GetUserRequest.UserName = user.Email;
            listToDosResult.GetUserRequest.Password = accountHelper.BtoaEquivalent(user.Password);
            listToDosResult.GetUserRequest.UserID = user.UserID;
            //Add essentials

            return View(listToDosResult);
        }

        [HttpPost("/Account/Connect")]
        public async Task<IActionResult> Connect([FromBody] AddConnectionRequest model)
        {

            var addConnectionResult = await _addConnectionHandler.Execute(new AddConnectionRequest { 
                ConnectionUserID = model.ConnectionUserID, UserID = model.UserID });

            return Ok(addConnectionResult);
        }

        [HttpGet("/Account/Connect")]
        public async Task<IActionResult> Connect(string userName, string token, string path)
        {
            var user = await _getUserDetailsQuery.Query(new GetUserDetailsRequest { UserName = userName, Email = userName });

            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            //Logged in?
            var loggedIn = accountHelper.checkLoggedIn(userName, token);
            if (loggedIn == false) { return RedirectToAction("Login", "Account"); }
            //Logged in?


            var listToDosResult = new ListToDosResult();
            listToDosResult.GetUserRequest = new GetUserRequest();
            //List comment count for the comments 
            //Get a list of connections
            var listPossibleConnectionsQueryResult = await _listPossibleConnectionsQuery.Query(new ListPossibleConnectionsRequest { UserID = user.UserID });

            //Get a list of connections
            //Newly added
            listToDosResult.ListPossibleConnectionsResult = listPossibleConnectionsQueryResult;
            listToDosResult.GetUserDetailsResult = user;

            listToDosResult.GetUserRequest.Email = user.Email;
            listToDosResult.GetUserRequest.UserName = user.Email;
            listToDosResult.GetUserRequest.Password = accountHelper.BtoaEquivalent(user.Password);
            listToDosResult.GetUserRequest.UserID = user.UserID;

            return View(listToDosResult);
        }

        //[HttpPost("/Account/Connect")]
        //public async Task<IActionResult> Connect([FromBody] AddConnectionRequest model)
        //{

        //}

        [HttpGet("/Account/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("/Account/Edit")]
        public async Task<ActionResult> Edit(string userName, string token)
        {
            var user = await _getUserDetailsQuery.Query(new GetUserDetailsRequest { UserName = userName, Email = userName });

            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            //Logged in?
            var loggedIn = accountHelper.checkLoggedIn(userName, token);
            if (loggedIn == false) { return RedirectToAction("Login", "Account"); }
            //Logged in?

            var listToDosResult = new ListToDosResult();
            listToDosResult.GetUserRequest = new GetUserRequest();
            //List comment count for the comments 

            //Newly added
            listToDosResult.GetUserDetailsResult = user;

            listToDosResult.GetUserRequest.Email = user.Email;
            listToDosResult.GetUserRequest.UserName = user.Email;
            listToDosResult.GetUserRequest.Password = accountHelper.BtoaEquivalent(user.Password);
            listToDosResult.GetUserRequest.UserID = user.UserID;

            return View(listToDosResult);
        }

        [HttpPost("/Account/Edit")]
        public async Task<ActionResult> Edit([FromBody] EditUserRequest model)
        {
            var editUserResult = await _editUserHandler.Execute(model);

            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            var listToDosResult = accountHelper.ListTodosQueryResult(model.Email, model.Email, model.UserId);

            //Get user details and password

            var userDetails = await _getUserDetailsQuery.Query(new GetUserDetailsRequest { Email = model.Email });

            listToDosResult.GetUserRequest.Password = userDetails.Password; //password
            listToDosResult.GetUserRequest.UserID = model.UserId;

            return Ok(listToDosResult);
        }

        [HttpPost("/Account/Create")]
        public async Task<ActionResult> CreateAccount([FromBody] AddUserRequest model)
        {
            var addUserHandlerResult = await _addUserHandler.Execute(model);
            var response = addUserHandlerResult;

            return Ok(response);
        }

        [HttpGet("/Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Account/Login")]
        public async Task<ActionResult> Login([FromBody] GetUserRequest model)
        {
            model.Email = model.Email.Trim();
            model.Password = model.Password.Trim();

            var user = await _getUserQuery.Query(model);

            return Ok(user);
        }

        //[HttpPost("/Account/Index")]
        //public async Task<IActionResult> Index([FromBody] GetUserRequest model)
        //{
        //    return Ok();
        //}


        [HttpGet("/Account/Index")]
        public async Task<IActionResult> Index(string email, string username, string token, int userID)
        {
            try
            {
                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

                var listToDosResult = accountHelper.ListTodosQueryResult(email, username, userID);

                //List comment count for the comments 

                //Newly added
                listToDosResult.GetUserRequest.Password = token;
                listToDosResult.GetUserRequest.UserID = userID;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                return View(listToDosResult);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet("/Account/ViewConnectionTasks")]
        public async Task<IActionResult> ViewConnectionTasks(string email, string username, string token, int connectionID, int userID, string taskEmail)
        {
            try
            {
                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

                var listToDosResult = accountHelper.ListTodosQueryResult(taskEmail, taskEmail, connectionID);

                //List comment count for the comments 

                //Newly added
                listToDosResult.GetUserRequest.Password = token;

                listToDosResult.GetUserRequest.TasksView = true;
                listToDosResult.GetUserRequest.ConnectionID = connectionID;
                listToDosResult.GetUserRequest.TaskEmail = taskEmail;

                listToDosResult.GetUserRequest.Email = email;
                listToDosResult.GetUserRequest.UserName = username;
                listToDosResult.GetUserRequest.UserID = userID;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                return View(listToDosResult);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Login", "Account");
        }

        //[HttpGet("/Account/AnotherIndex")]
        //public async Task<IActionResult> Main(string email, string username)
        //{
        //    var getUserRequest = new GetUserRequest();

        //    getUserRequest.Email = email;
        //    getUserRequest.UserName = username;

        //    var listTodosQueryResult = await _listTodosQuery.Query(new ListTodosRequest
        //    {
        //        Page = 1,
        //        PageSize = 7,
        //        Search = ""
        //    });

        //    var listToDosResult = new ListToDosResult
        //    {
        //        ListTodosResult = listTodosQueryResult,
        //        GetUserRequest = getUserRequest
        //    };

        //     return View(listToDosResult);
        //}

        //[HttpGet]
        //public async Task<ActionResult> Landing([FromBody] GetUserRequest model)
        //{
        //    return View(model);
        //}
    }
}
