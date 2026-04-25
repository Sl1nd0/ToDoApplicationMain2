using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Messages.Database
{
    public class MessagesDbDesignContext : IDesignTimeDbContextFactory<MessagesDbContext>
    {
        public MessagesDbContext CreateDbContext(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            var config = builder.Build();
            var connStr = config.GetConnectionString("DefaultConnection");
            var contextBuilder = new DbContextOptionsBuilder<MessagesDbContext>();

            if (config.GetValue<string>("DbType").ToUpper().Equals("SQL"))
                return new MessagesDbContext(contextBuilder.UseSqlServer(connStr).Options);

            if (config.GetValue<string>("DbType").ToUpper().Equals("INMEMORY"))
                return new MessagesDbContext(contextBuilder.UseInMemoryDatabase(connStr).Options);

            return null;

        }
    }
}
