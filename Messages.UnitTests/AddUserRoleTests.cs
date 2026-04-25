using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.UnitTests
{
    [TestClass]
    public class AddUserRoleTests
    {
        [TestMethod]
        public void AddUserRoleSuccess()
        {
            var task = TestAssembly.services.GetRequiredService<ICommandHandler<AddUserRoleRequest>>();

            var addUserRoleRequest = new AddUserRoleRequest
            {
                RoleName = "Admin",
            };

            var handler = task.Execute(addUserRoleRequest);
            handler.Wait();

            addUserRoleRequest = new AddUserRoleRequest
            {
                RoleName = "User",
            };

            handler = task.Execute(addUserRoleRequest);
            handler.Wait();

            var result = handler.Result;
            return;
        }


        [TestMethod]
        public void GetUserRoleSuccess()
        {
            AddUserRoleSuccess();

            var task = TestAssembly.services
                                    .GetRequiredService<IQuery<GetUserRoleRequest, GetUserRoleResult>>();

            var userRoleQuery = task.Query(new GetUserRoleRequest
            {
                RoleName = "Admin"
            });

            var result = userRoleQuery.Result;

            return;
        }
    }
}
