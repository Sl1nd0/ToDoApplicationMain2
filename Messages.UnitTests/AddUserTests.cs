using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.Extensions.DependencyInjection;

namespace Messages.UnitTests
{
    [TestClass]
    public class AddUserTests
    {

        [TestMethod]
        public void AddUserSuccess()
        {
            var addUserRole = new AddUserRoleTests();
            addUserRole.AddUserRoleSuccess();

            var task = TestAssembly.services.GetRequiredService<Services.Core.Interfaces.ICommandHandler<Messages.Shared.DTO.Requests.CommandRequests.AddUserRequest>>();

            var addUserRequest = new Messages.Shared.DTO.Requests.CommandRequests.AddUserRequest
            {
                UserName = "TestUser",
                Name = "TestUserName",
                Surname = "TestUserSur",
                CellNumber = "TestUserCell",
                Email = "ssankabi@gmail.com",
                DateOfBirth = "1995-10-01",
                Identifier = "ssanko",
                Password = "ssanko",
            };


            //User 2
           var handler = task.Execute(addUserRequest);
            handler.Wait();


            addUserRequest = new Messages.Shared.DTO.Requests.CommandRequests.AddUserRequest
            {
                UserName = "TestUser2",
                Name = "TestUserName2",
                Surname = "TestUserSur2",
                CellNumber = "TestUserCell2",
                Email = "ssankabi@gmail.com4",
                DateOfBirth = "1995-10-01",
                Identifier = "ssanko2",
                Password = "ssanko2",
            };

            handler = task.Execute(addUserRequest);
            handler.Wait();
            //User 2

            addUserRequest = new Messages.Shared.DTO.Requests.CommandRequests.AddUserRequest
            {
                UserName = "TestUser2d3",
                Name = "TestUserName23",
                Surname = "TestUserSur2d3",
                CellNumber = "TestUserCell2d3",
                Email = "ssankabi@gmail.comd5",
                DateOfBirth = "1995-10-01",
                Identifier = "ssanko2d3",
                Password = "ssanko2d3",
            };

            handler = task.Execute(addUserRequest);
            handler.Wait();
            //User 3

            addUserRequest = new Messages.Shared.DTO.Requests.CommandRequests.AddUserRequest
            {
                UserName = "TestUser23",
                Name = "TestUserName23",
                Surname = "TestUserSur23",
                CellNumber = "TestUserCell23",
                Email = "ssankabi@gmail.com4",
                DateOfBirth = "1995-10-01",
                Identifier = "ssanko23",
                Password = "ssanko23",
            };

            handler = task.Execute(addUserRequest);
            handler.Wait();
            //User 4

            addUserRequest = new Messages.Shared.DTO.Requests.CommandRequests.AddUserRequest
            {
                UserName = "TestUser234",
                Name = "TestUserName234",
                Surname = "TestUserSur253",
                CellNumber = "TestUserCell235",
                Email = "ssankabi@gmail.com545",
                DateOfBirth = "1995-10-01",
                Identifier = "ssanko2354",
                Password = "ssanko2345",
            };

            handler = task.Execute(addUserRequest);
            handler.Wait();
            //User 5

            var result = handler.Result;

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void GetUserQuerySuccess()
        {
            AddUserSuccess();

            var task = TestAssembly.services.GetRequiredService<Services.Core.Interfaces.IQuery<GetUserRequest, GetUserResult>>();

            var userQuery = task.Query(new GetUserRequest
            {
                UserName = "TestUser",
                Password = "ssanko"
            });
            userQuery.Wait();

            var result = userQuery.Result;

            Assert.IsNotNull(result.UserName);
        }
    }
}
