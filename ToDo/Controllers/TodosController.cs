using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Services.Core.Interfaces;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Web;
using ToDo.Helpers;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class TodosController : Controller
    {
        private readonly ICommandHandler<AddTodosRequest> _addTodosHandler;
        private readonly ICommandHandler<AddUserRequest> _addUserHandler;
        private readonly ICommandHandler<EditTodosRequest> _editTodosHandler;
        private readonly IQuery<ListCommentsByTodoIdRequest, List<ListCommentsByTodoIdResult>> _listCommentsByTodoIdResultQuery;
        private readonly IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult> _countCommentsByToDoIdQuery;
        private readonly ICommandHandler<DeleteCommentRequest> _deleteCommentHandler;
        private readonly ICommandHandler<EditCommentRequest> _editCommentHandler;
        private readonly IQuery<ListTodosRequest, List<ListTodosResult>> _listTodosQuery;
        private readonly ICommandHandler<DeleteTodosRequest> _deleteTodosHandler;
        private readonly ICommandHandler<AddCommentTodoRequest> _addCommentTodoHandler;
        private readonly IQuery<GetUserRequest, GetUserResult> _getUserQuery;

        public TodosController(ICommandHandler<AddTodosRequest> addTodosHandler,
           ICommandHandler<AddUserRequest> addUserHandler,
            IQuery<ListTodosRequest, List<ListTodosResult>> listTodosQuery,
            ICommandHandler<DeleteTodosRequest> deleteTodosHandler,
            ICommandHandler<AddCommentTodoRequest> AddCommentTodoHandler,
            ICommandHandler<EditTodosRequest> editTodosHandler,
            IQuery<ListCommentsByTodoIdRequest, List<ListCommentsByTodoIdResult>> listCommentsByTodoIdResultQuery,
            IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult> countCommentsByToDoIdQuery,
            ICommandHandler<DeleteCommentRequest> deleteCommentHandler,
             ICommandHandler<EditCommentRequest> editCommentHandler,
            IQuery<GetUserRequest, GetUserResult> getUserQuery)
        {
            _addTodosHandler = addTodosHandler;
            _addUserHandler = addUserHandler;
            _editTodosHandler = editTodosHandler;
            _listCommentsByTodoIdResultQuery = listCommentsByTodoIdResultQuery;
            _countCommentsByToDoIdQuery = countCommentsByToDoIdQuery;
            _deleteCommentHandler = deleteCommentHandler;
            _editCommentHandler = editCommentHandler;
            _listTodosQuery = listTodosQuery;
            _deleteTodosHandler = deleteTodosHandler;
            _addCommentTodoHandler = AddCommentTodoHandler;
            _getUserQuery = getUserQuery;
        }

        [HttpGet]
        public IActionResult Create(string email, string username, string token, int userID)
        {

            try
            {
                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

                var ListToDosResult = accountHelper.ListTodosQueryResult(email, username, userID);

                ListToDosResult.GetUserRequest.Password = token;
                ListToDosResult.GetUserRequest.UserID = userID;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                return View(ListToDosResult);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] GetUserRequest model)
        {
            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);
            //valid account
            var ListToDosResult = accountHelper.ListTodosQueryResult(model.Email, model.UserName, model.UserID);

            return Ok(ListToDosResult);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddTodosRequest model)
        {

            //get user id 1st 
            var addTodosHandlerResult = await _addTodosHandler.Execute(model);
            var listToDosResult = new ListTodosResult();
            var viewlistToDosResult = new ListToDosResult();
            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            try
            {
                if (addTodosHandlerResult.Success)
                {
                    viewlistToDosResult = accountHelper.ListTodosQueryResult(model.UserName, model.UserName, 0);

                    //return RedirectToAction("Index", "Account",
                    //    new
                    //    {
                    //        email = model.UserName,
                    //        username = model.UserName
                    //    });
                    return Ok(addTodosHandlerResult);
                    //return View(viewlistToDosResult);
                }
                else
                {
                    throw new Exception("An error occurred while adding the task. Please try again.");
                }
            }
            catch (Exception ex)
            {
                listToDosResult.Error = ex.Message;
                viewlistToDosResult.ListTodosResult.Add(listToDosResult);
                viewlistToDosResult.GetUserRequest.Email = model.UserName;
                viewlistToDosResult.GetUserRequest.UserName = model.UserName;

                return View(viewlistToDosResult);
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Logout()
        //{
        //    return RedirectToAction("Login", "Account");
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentDto model)
        {
            var deleteCommentHandlerResult = await _deleteCommentHandler.Execute(
                new DeleteCommentRequest { Id = model.CommentID });

            return Ok(deleteCommentHandlerResult);
        }

        [HttpGet]
        public async Task<IActionResult> EditComment(string comment, int commentID, string username, string todoTitle, string todo, string token, int toDoID, int userID)
        {
            //string email 1, string username 1, string todo 1, string title 1, string token 1, int toDoID 1, int userID 1
            //string comment, string username, string todoTitle, string todo, string title, string token, int toDoID, int userID
            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            var loggedIn = accountHelper.checkLoggedIn(username, token);

            if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

            var editCommentHelperDto = new EditCommentHelperDto();


            editCommentHelperDto.UserName = username;
            editCommentHelperDto.Email = username;
            editCommentHelperDto.ToDoTitle = todoTitle;
            editCommentHelperDto.ToDo = todo;
            editCommentHelperDto.Password = token;
            editCommentHelperDto.ToDoId = toDoID;
            editCommentHelperDto.UserID = userID;
            editCommentHelperDto.Comment = comment;


            editCommentHelperDto.CommentID = commentID;

            var listToDosResult = new ListToDosResult();
            listToDosResult.GetUserRequest = new GetUserRequest();

            listToDosResult.GetUserRequest.Email = username;
            listToDosResult.GetUserRequest.Password = "";

            listToDosResult.EditCommentHelperDto = editCommentHelperDto;

            //Comment(string email, string username, string todo, string title, string token, int toDoID, int userID)
            return View(listToDosResult);
        }
        

        [HttpGet]
        public async Task<IActionResult> EditCommentANew(string comment, int commentID, string username, string todoTitle, string todo, string token, int toDoID, int userID, string taskEmail, int connectionID, string taskView)
        {
            //string email 1, string username 1, string todo 1, string title 1, string token 1, int toDoID 1, int userID 1
            //string comment, string username, string todoTitle, string todo, string title, string token, int toDoID, int userID
            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            var loggedIn = accountHelper.checkLoggedIn(username, token);

            if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

            var editCommentHelperDto = new EditCommentHelperDto();


            editCommentHelperDto.UserName = username;
            editCommentHelperDto.Email = username;
            editCommentHelperDto.ToDoTitle = todoTitle;
            editCommentHelperDto.ToDo = todo;
            editCommentHelperDto.Password = token;
            editCommentHelperDto.ToDoId = toDoID;
            editCommentHelperDto.UserID = userID;
            editCommentHelperDto.Comment = comment;


            editCommentHelperDto.CommentID = commentID;

            var listToDosResult = new ListToDosResult();
            listToDosResult.GetUserRequest = new GetUserRequest();

            listToDosResult.GetUserRequest.Email = username;
            listToDosResult.GetUserRequest.Password = "";
            listToDosResult.GetUserRequest.TaskEmail = taskEmail;
            listToDosResult.GetUserRequest.ConnectionID = connectionID;

            listToDosResult.EditCommentHelperDto = new EditCommentHelperDto();

            listToDosResult.EditCommentHelperDto = editCommentHelperDto;

            //Comment(string email, string username, string todo, string title, string token, int toDoID, int userID)
            return View(listToDosResult);
        }

        [HttpPost]
        public async Task<ActionResult> EditComment([FromBody] EditCommentHelperDto model)
        {
            //string comment, string username, string todoTitle, string todo, string title, string token, int toDoID, int userID
            var editCommentResult = await _editCommentHandler.Execute(new EditCommentRequest { Comment = model.Comment, Id = model.CommentID });

            var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);

            var loggedIn = accountHelper.checkLoggedIn(model.Email, model.Password);

            if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

            return Ok(editCommentResult);
        }

        [HttpGet]
        public async Task<IActionResult> CommentNew(string email, string username, string todo, string title, string token, int toDoID, int userID, bool taskView, int connectionID, string taskEmail)
        {
            try
            {
                var myemail = HttpUtility.UrlDecode(email);
                var myuser = HttpUtility.UrlDecode(username);
                var mytodo = HttpUtility.UrlDecode(todo);
                var mytitle = HttpUtility.UrlDecode(title);


                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);
                var ListToDosResult = accountHelper.ListTodosQueryResult(myemail, myuser, userID);

                ListToDosResult.GetUserRequest.Password = token;
                ListToDosResult.GetUserRequest.TasksView = taskView;
                ListToDosResult.GetUserRequest.ConnectionID = connectionID;
                ListToDosResult.GetUserRequest.UserID = userID;
                ListToDosResult.GetUserRequest.TaskEmail = taskEmail;
                ListToDosResult.GetUserRequest.ToDoID = toDoID;
                ListToDosResult.GetUserRequest.ToDo = todo;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                //Query list of comments
                var listCommentsByTodoIdResult = await _listCommentsByTodoIdResultQuery.Query(
                                                        new ListCommentsByTodoIdRequest { Page = 1, PageSize = 7, ToDoID = toDoID, Search = "" });

                for (var i = 0; i < listCommentsByTodoIdResult.Count(); i++)
                {
                    listCommentsByTodoIdResult[i].CommentCleaned = listCommentsByTodoIdResult[i].Comment.Replace("'", "q1");
                    listCommentsByTodoIdResult[i].CommentCleaned = listCommentsByTodoIdResult[i].CommentCleaned.Replace("\"", "q2");
                }

                var commentUserHelperDto = new CommentUserHelperDto();

                commentUserHelperDto.Email = myemail;
                commentUserHelperDto.UserName = myuser;
                commentUserHelperDto.ToDo = mytodo;
                commentUserHelperDto.ToDoID = toDoID;
                commentUserHelperDto.UserID = userID;
                commentUserHelperDto.ToDoTitle = mytitle;
                commentUserHelperDto.ListCommentsByTodoIdResult = listCommentsByTodoIdResult;



                ListToDosResult.CommentUserHelperDto = commentUserHelperDto;

                return View(ListToDosResult);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Comment(string email, string username, string todo, string title, string token, int toDoID, int userID)
        {
            try
            {
                var myemail = HttpUtility.UrlDecode(email);
                var myuser = HttpUtility.UrlDecode(username);
                var mytodo = HttpUtility.UrlDecode(todo);
                var mytitle = HttpUtility.UrlDecode(title);


                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);
                var ListToDosResult = accountHelper.ListTodosQueryResult(myemail, myuser, userID);

                ListToDosResult.GetUserRequest.Password = token;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                //Query list of comments
                var listCommentsByTodoIdResult = await _listCommentsByTodoIdResultQuery.Query(
                                                        new ListCommentsByTodoIdRequest { Page = 1, PageSize = 7, ToDoID = toDoID, Search = "" });

                for (var i = 0; i < listCommentsByTodoIdResult.Count(); i++)
                {
                    listCommentsByTodoIdResult[i].CommentCleaned = listCommentsByTodoIdResult[i].Comment.Replace("'", "q1");
                    listCommentsByTodoIdResult[i].CommentCleaned = listCommentsByTodoIdResult[i].CommentCleaned.Replace("\"", "q2");
                }

                var commentUserHelperDto = new CommentUserHelperDto();

                commentUserHelperDto.Email = myemail;
                commentUserHelperDto.UserName = myuser;
                commentUserHelperDto.ToDo = mytodo;
                commentUserHelperDto.ToDoID = toDoID;
                commentUserHelperDto.UserID = userID;
                commentUserHelperDto.ToDoTitle = mytitle;
                commentUserHelperDto.ListCommentsByTodoIdResult = listCommentsByTodoIdResult;



                ListToDosResult.CommentUserHelperDto = commentUserHelperDto;

                return View(ListToDosResult);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //ToDo: Here
        [HttpPost]
        public async Task<ActionResult> Comment([FromBody] AddCommentTodoRequest model)
        {
            //ICommandHandler<EditTodosRequest>
            var myemail = HttpUtility.UrlDecode(model.UserName);
            model.ToDo = HttpUtility.UrlDecode(model.ToDo);
            model.ToDoTitle = HttpUtility.UrlDecode(model.ToDoTitle);

            //Edit the title and ToDo and return
            var result = await _addCommentTodoHandler.Execute(model);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Edit(string email, string username, string todo, string title, int todoID, string token, int userID)
        {
            try
            {
                var myemail = HttpUtility.UrlDecode(email);
                var myuser = HttpUtility.UrlDecode(username);
                var mytodo = HttpUtility.UrlDecode(todo);
                var mytitle = HttpUtility.UrlDecode(title);

                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);
                var ListToDosResult = accountHelper.ListTodosQueryResult(myemail, myuser, userID);

                //Newly added
                ListToDosResult.GetUserRequest.Password = token;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                var editUserHelperDto = new EditUserHelperDto();

                editUserHelperDto.Email = myemail;
                editUserHelperDto.UserName = myuser;
                editUserHelperDto.ToDo = mytodo;
                editUserHelperDto.ToDoTitle = mytitle;
                editUserHelperDto.ToDoID = todoID;
                editUserHelperDto.UserID = userID;

                ListToDosResult.EditUserHelperDto = editUserHelperDto;

                return View(ListToDosResult);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //ToDo: Here
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] EditTodosRequest model)
        {
            //ICommandHandler<EditTodosRequest>
            var myemail = HttpUtility.UrlDecode(model.UserName);
            model.ToDo = HttpUtility.UrlDecode(model.ToDo);
            model.ToDoTitle = HttpUtility.UrlDecode(model.ToDoTitle);

            //Edit the title and ToDo and return
            var result = await _editTodosHandler.Execute(model);

            return Ok(result);
        }

        //ToDo: Here
        [HttpPost]
        public async Task<ActionResult> Delete([FromBody] DeleteTodosRequest model)
        {
            //ICommandHandler<EditTodosRequest>
            var myemail = HttpUtility.UrlDecode(model.UserName);
            model.ToDo = HttpUtility.UrlDecode(model.ToDo);
            model.ToDoTitle = HttpUtility.UrlDecode(model.ToDoTitle);

            //Edit the title and ToDo and return
            var result = await _deleteTodosHandler.Execute(model);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Delete(string email, string username, string todo, string title, int todoID, string token, int userID)
        {
            try
            {

                var myemail = HttpUtility.UrlDecode(email);
                var myuser = HttpUtility.UrlDecode(username);
                var mytodo = HttpUtility.UrlDecode(todo);
                var mytitle = HttpUtility.UrlDecode(title);

                var accountHelper = new AccountHelper(_addUserHandler, _listTodosQuery, _getUserQuery, _countCommentsByToDoIdQuery);
                var ListToDosResult = accountHelper.ListTodosQueryResult(myemail, myuser, userID);

                ListToDosResult.GetUserRequest.Password = token;

                var loggedIn = accountHelper.checkLoggedIn(email, token);

                if (loggedIn == false) { return RedirectToAction("Login", "Account"); }

                var deleteUserHelperDto = new DeleteUserHelperDto();

                deleteUserHelperDto.Email = myemail;
                deleteUserHelperDto.UserName = myuser;
                deleteUserHelperDto.ToDo = mytodo;
                deleteUserHelperDto.ToDoTitle = mytitle;
                deleteUserHelperDto.ToDoID = todoID;
                deleteUserHelperDto.UserID = userID;

                ListToDosResult.DeleteUserHelperDto = deleteUserHelperDto;

                return View(ListToDosResult);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }

        }

    }
}
