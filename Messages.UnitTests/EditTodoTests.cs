using Messages.Shared.DTO.Requests.CommandRequests;
using Messages.Shared.DTO.Requests.QueryRequests;
using Messages.Shared.DTO.Results.QueryResults;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;

namespace Messages.UnitTests
{
    [TestClass]
    public class EditTodoTests
    {
        [TestMethod]
        public void ListTodosQuerySuccess()
        {
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
