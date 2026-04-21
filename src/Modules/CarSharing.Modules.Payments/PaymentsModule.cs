using Autofac;
using CarSharing.Modules.Payments.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharing.Modules.Payments;

public sealed class PaymentsModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx =>
            {
                var options = new DbContextOptionsBuilder<PaymentsDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("PaymentsDb")!)
                    .Options;

                return new PaymentsDbContext(options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t =>
                t.Name.EndsWith("Service", StringComparison.Ordinal) &&
                t.Namespace is not null &&
                t.Namespace.StartsWith("CarSharing.Modules.Payments", StringComparison.Ordinal))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
