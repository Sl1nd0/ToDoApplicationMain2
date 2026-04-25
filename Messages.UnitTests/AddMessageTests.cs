using Messages.Shared.DTO.Requests.CommandRequests;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;

namespace Messages.UnitTests
{
    [TestClass]
    public class AddMessageTests
    {
        [TestMethod]
        public void AddMessageSuccess()
        {
            var task = TestAssembly.services.GetRequiredService<ICommandHandler<AddMessageRequest>>();

            var addMessageRequest = new AddMessageRequest
            {
                Text = "Hello3",
            };

            var handler = task.Execute(addMessageRequest);
            handler.Wait();
            var result = handler.Result;

        }
    }
}