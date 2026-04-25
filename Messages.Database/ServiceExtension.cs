using Messages.Database.Repository;
using Messages.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Messages.Database
{
    public static class ServiceExtension
    {
        public static void AddMessagesDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<string>("DbType").ToUpper().Equals("SQL"))
                services.AddDbContext<MessagesDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            if (configuration.GetValue<string>("DbType").ToUpper().Equals("INMEMORY"))
                services.AddDbContext<MessagesDbContext>(options => options.UseInMemoryDatabase(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IdentityDbContext, MessagesDbContext>();
            services.AddScoped<DbContext, MessagesDbContext>();
            services.AddScoped<IMessagesDomain, MessagesDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            AddRepositoryService(services);
            services.AddDomainServices();
        }

        public static void AddRepositoryService(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
