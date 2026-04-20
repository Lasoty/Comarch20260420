using Autofac;
using CarSharing.Modules.Fleet.Infrastructure;
using CarSharing.Modules.Fleet.PublicApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarSharing.Modules.Fleet;

public sealed class FleetModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx =>
            {
                var options = new DbContextOptionsBuilder<FleetDbContext>()
                    .UseSqlServer(configuration.GetConnectionString("FleetDb")!)
                    .Options;

                return new FleetDbContext(options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t =>
                t.Name.EndsWith("Service", StringComparison.Ordinal) &&
                t.Namespace is not null &&
                t.Namespace.StartsWith("CarSharing.Modules.Fleet", StringComparison.Ordinal))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
