using Microsoft.Extensions.DependencyInjection;

namespace Messages.UnitTests
{
    [TestClass]
    public class UpdateUserRoleHandlerTests
    {
        [TestMethod]
        public void UpdateUserRoleSuccess()
        {
            var add = new AddUserRoleTests();
            add.AddUserRoleSuccess();

            var task = TestAssembly.services.GetRequiredService<Services.Core.Interfaces.ICommandHandler<Messages.Shared.DTO.Requests.CommandRequests.UpdateUserRoleRequest>>();

            var updateUserRoleRequest = new Messages.Shared.DTO.Requests.CommandRequests.UpdateUserRoleRequest
            {
                Id = 1,
                RoleName = "SuperAdmin",
            };

            var handler = task.Execute(updateUserRoleRequest);
            handler.Wait();

            var result = handler.Result;
        }
    }
}
