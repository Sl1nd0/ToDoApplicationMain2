using Microsoft.Extensions.DependencyInjection;

namespace Messages.UnitTests
{
    [TestClass]
    public class DeleteUserRolesTests
    {
        [TestMethod]
        public void DeleteUserRoleSuccess()
        {
            var add = new AddUserRoleTests();
            add.AddUserRoleSuccess();

            var task = TestAssembly.services.GetRequiredService<Services.Core.Interfaces.ICommandHandler<Messages.Shared.DTO.Requests.CommandRequests.DeleteUserRoleRequest>>();
            var deleteUserRoleRequest = new Messages.Shared.DTO.Requests.CommandRequests.DeleteUserRoleRequest
            {
                Id = 1,
            };
            var handler = task.Execute(deleteUserRoleRequest);
            handler.Wait();

            var result = handler.Result;
        }
    }
}
