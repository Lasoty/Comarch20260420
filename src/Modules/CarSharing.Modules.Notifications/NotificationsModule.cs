using Autofac;
using CarSharing.Modules.Notifications.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharing.Modules.Notifications;

public sealed class NotificationsModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx =>
            {
                var options = new DbContextOptionsBuilder<NotificationsDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("NotificationsDb")!)
                    .Options;

                return new NotificationsDbContext(options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t =>
                t.Name.EndsWith("Service", StringComparison.Ordinal) &&
                t.Namespace is not null &&
                t.Namespace.StartsWith("CarSharing.Modules.Notifications", StringComparison.Ordinal))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
