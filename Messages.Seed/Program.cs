using Messages.Seed;
using Messages.Shared.DTO.Requests.CommandRequests;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var svc = new ServiceCollection();
        RegisterServices.Register(svc);
        var services = svc.BuildServiceProvider();

        var addTask = services.GetRequiredService<ICommandHandler<AddUserRoleRequest>>();

        var addUserRoleRequest = new AddUserRoleRequest
        {
            RoleName = "Admin",
        };

        var addResult = addTask.Execute(addUserRoleRequest);
        addResult.Wait();

        var result = addResult.Result;

        Console.WriteLine("Hello, World!");
    }

}
