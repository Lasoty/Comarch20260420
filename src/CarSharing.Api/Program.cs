using Autofac;
using Autofac.Extensions.DependencyInjection;
using CarSharing.BuildingBlocks;
using CarSharing.Modules.Fleet;
using CarSharing.Modules.Fleet.Infrastructure;
using CarSharing.Modules.Notifications;
using CarSharing.Modules.Notifications.Infrastructure;
using CarSharing.Modules.Reservations;
using CarSharing.Modules.Reservations.Infrastructure;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CarSharing.Modules.Fleet.Controllers.FleetController).Assembly)
    .AddApplicationPart(typeof(CarSharing.Modules.Reservations.Controllers.ReservationsController).Assembly)
    .AddApplicationPart(typeof(CarSharing.Modules.Notifications.Controllers.NotificationsController).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(FleetModule).Assembly,
        typeof(ReservationsModule).Assembly,
        typeof(NotificationsModule).Assembly);
});

builder.Services.AddValidatorsFromAssemblyContaining<
    CarSharing.Modules.Fleet.Application.AddCar.AddCarCommandValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<
    CarSharing.Modules.Reservations.Application.CreateReservation.CreateReservationCommandValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new FleetModule(builder.Configuration));
    containerBuilder.RegisterModule(new ReservationsModule(builder.Configuration));
    containerBuilder.RegisterModule(new NotificationsModule(builder.Configuration));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<FleetDbContext>().Database.EnsureCreated();
    scope.ServiceProvider.GetRequiredService<ReservationsDbContext>().Database.EnsureCreated();
    scope.ServiceProvider.GetRequiredService<NotificationsDbContext>().Database.EnsureCreated();
}

app.Run();
