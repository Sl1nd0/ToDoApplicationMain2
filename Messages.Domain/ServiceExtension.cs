using Messages.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Interfaces;
using System.Reflection;

namespace Messages.Domain
{
    public static class ServiceExtension
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            var assembly = typeof(Message).GetTypeInfo().Assembly;

            var handlers = assembly.GetTypes().Where(m => !m.IsInterface && m.Name.EndsWith("Handler"));

            foreach (var item in handlers)
            {
                var inter = item.GetInterfaces().Where(b => b.Name.StartsWith("ICommandHandler")).FirstOrDefault();
                if (inter == null) continue;

                var vt = typeof(ICommandHandler<>);
                var gt = vt.MakeGenericType(inter.GenericTypeArguments[0]);
                services.AddScoped(gt, item);
                services.AddScoped(item);
            }

            var results = assembly.GetTypes().Where(m => !m.IsInterface);

            foreach (var repo in results)
            {
                var inter = repo.GetInterfaces().Where(b => b.Name.StartsWith("IQuery") && !b.Name.StartsWith("IQueryHandler") && !b.Name.StartsWith("IQueryModel")).FirstOrDefault();
                if (inter == null) continue;

                var vt = typeof(IQuery<,>);
                var ta = new Type[2];
                ta[0] = inter.GenericTypeArguments[0];
                ta[1] = inter.GenericTypeArguments[1];
                var gt = vt.MakeGenericType(ta);
                services.AddScoped(gt, repo);
                services.AddScoped(repo);
            }
        }

    }
}
