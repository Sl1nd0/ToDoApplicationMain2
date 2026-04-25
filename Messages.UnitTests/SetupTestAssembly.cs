using Microsoft.Extensions.DependencyInjection;

namespace Messages.UnitTests
{
    [TestClass]
    public class SetupTestAssembly
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var svc = new ServiceCollection();
            RegisterServices.Register(svc);
            TestAssembly.services = svc.BuildServiceProvider();
        }

        public void SetupTestData()
        {

        }
    }

    public static class TestAssembly
    {
        public static ServiceProvider services;
    }
}

