using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FleetSharing.Modules.Fleet.Autofac;

public class FleetModule(ConfigurationManager builderConfiguration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        //builder.Register(ctx =>
        //    {
        //        var options = new DbContextOptionsBuilder<FleetDbContext>()
        //            .UseSqlServer(configuration.GetConnectionString("FleetDb")!)
        //            .Options;

        //        return new FleetDbContext(options);
        //    })
        //    .AsSelf()
        //    .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(FleetModule).Assembly)
            .Where(t =>
                t.Name.EndsWith("Service", StringComparison.Ordinal) &&
                t.Namespace is not null &&
                t.Namespace.StartsWith("FleetSharing.Modules.Fleet"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
