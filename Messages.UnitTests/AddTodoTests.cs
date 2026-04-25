using Messages.Domain.Entities;
using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;

namespace Messages.UnitTests
{
    [TestClass]
    public class AddTodoTests
    {
        [TestMethod]
        public void AddTodoSuccess()
        {
            var addUserHandler = new AddUserTests();

            addUserHandler.AddUserSuccess();

            var task = TestAssembly.services.GetRequiredService<ICommandHandler<AddTodosRequest>>();

            var addTodoRequest = new AddTodosRequest
            {
                ToDo = "Test",
                Title = "TestVToDoTitle",
                UsersID = 1,
                Email = "ssankabi@gmail.com",
            };

            var addToDo = task.Execute(addTodoRequest);

            addToDo.Wait();

            var result = addToDo.Result;

            Assert.IsTrue(result.Success);
        }


        [TestMethod]
        public void AddConnectionSuccess()
        {
            var addUserHandler = new AddUserTests();
            addUserHandler.AddUserSuccess();

            var task = TestAssembly.services.GetRequiredService<ICommandHandler<AddConnectionRequest>>();

            var request = new AddConnectionRequest
            {
                UserID = 1,
                ConnectionUserID = 2,
            };

            var handler = task.Execute(request);
            handler.Wait();
            
            request = new AddConnectionRequest
            {
                UserID = 1,
                ConnectionUserID = 4,
            };

            handler = task.Execute(request);
            handler.Wait();

            var result = handler.Result;

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ListPossibleConnectionsSuccess()
        {
            //var addUserHandler = new AddUserTests();

            //addUserHandler.AddUserSuccess();
            AddConnectionSuccess();

            var task = TestAssembly.services.GetRequiredService<IQuery<ListPossibleConnectionsRequest, List<ListPossibleConnectionsResult>>>();


            var listPossibleConnectionsResult = task.Query(new ListPossibleConnectionsRequest { UserID = 1 }); //3

            listPossibleConnectionsResult.Wait();

            var result = listPossibleConnectionsResult.Result;

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void ListConnectionsByUserSuccess()
        {
            AddConnectionSuccess();

            var task = TestAssembly.services.GetRequiredService<IQuery<ListConnectionsByUserIdRequest, List<ListConnectionsByUserIdResult>>>();

            var request = new ListConnectionsByUserIdRequest { UserID = 1};

            var query = task.Query(request);
            query.Wait();

            var result = query.Result;

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void DeleteConnectionSuccess()
        {
            AddConnectionSuccess();

            var task = TestAssembly.services.GetRequiredService<ICommandHandler<DeleteUserConnectionRequest>>();

            var request = new DeleteUserConnectionRequest { Id = 1};

            var handler = task.Execute(request);
            handler.Wait();

            var result = handler.Result;

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void AddTodoCommentSuccess()
        {
            AddTodoSuccess();

            var task = TestAssembly.services.GetRequiredService<ICommandHandler<AddCommentTodoRequest>>();

            var addTodoCommentRequest = new AddCommentTodoRequest
            {
                Comment = "Test",
                UserName = "",
                ToDoID = 1,
                userID = 1,
            };

            var addToDoComment = task.Execute(addTodoCommentRequest);

            addToDoComment.Wait();

            var result = addToDoComment.Result;

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void DeleteCommentSuccess()
        {
            AddTodoCommentSuccess();

            var task = TestAssembly.services.GetRequiredService<ICommandHandler<DeleteCommentRequest>>();

            var deleteCommentRequest = new DeleteCommentRequest
            {
                Id = 1
            };

            var handler = task.Execute(deleteCommentRequest);
            handler.Wait();

            var result = handler.Result;

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ListTodoCommentSuccess()
        {
            AddTodoCommentSuccess();

            var task = TestAssembly.services.GetRequiredService<IQuery<ListCommentsByTodoIdRequest, List<ListCommentsByTodoIdResult>>>();

            var listCommentsByTodoIdRequest = new ListCommentsByTodoIdRequest
            {
                Search = "",
                ToDoID = 1,
                Page = 1,
                PageSize = 7
            };

            var listCommentsByTodoIdQuery = task.Query(listCommentsByTodoIdRequest);

            listCommentsByTodoIdQuery.Wait();

            var result = listCommentsByTodoIdQuery.Result;

            //var result = addToDoComment.Result;

            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void CountTodoCommentSuccess()
        {
            AddTodoCommentSuccess();

            var task = TestAssembly.services.GetRequiredService<IQuery<CountCommentsByToDoIdRequest, CountCommentsByToDoIdResult>>();

            var countCommentsByTodoIdRequest = new CountCommentsByToDoIdRequest
            {
                ToDoID = 1
            };

            var countCommentsByTodoIdQuery = task.Query(countCommentsByTodoIdRequest);

            countCommentsByTodoIdQuery.Wait();

            var result = countCommentsByTodoIdQuery.Result;

            //var result = addToDoComment.Result;

            Assert.IsTrue(result.CommentsCount >= 0);
        }

        [TestMethod]
        public void EditTodoSuccess()
        {
            AddTodoSuccess();

            var task = TestAssembly.services.GetRequiredService<ICommandHandler<EditTodosRequest>>();
            var taskQuery = TestAssembly.services.GetRequiredService<IQuery<GetTodoRequest, GetTodoResult>>();

            var editTodoRequest = new EditTodosRequest
            {
                ToDo = "TestOneEdit",
                ToDoID = 1,
                ToDoTitle = "TestOneEdit",
                UserName = "Test",
            };

            var editToDo = task.Execute(editTodoRequest);

            editToDo.Wait();

            var result = editToDo.Result;

            var query = taskQuery.Query(new GetTodoRequest { Search = "TestOneEdit", Id = 1 });

            query.Wait();

            var queryResult = query.Result;

            Assert.IsNotNull(queryResult.ToDo);
        }

        [TestMethod]
        public void ListTodosQuerySuccess()
        {
            AddTodoSuccess();

            var task = TestAssembly.services.GetRequiredService<IQuery<ListTodosRequest, List<ListTodosResult>>>();

            var query = task.Query(new ListTodosRequest
            {
                Page = 1,
                PageSize = 1,
                Search = ""
            });

            query.Wait();

            var result = query.Result;

            Assert.IsNotNull(result.First()._ToDo);
        }
    }
}
