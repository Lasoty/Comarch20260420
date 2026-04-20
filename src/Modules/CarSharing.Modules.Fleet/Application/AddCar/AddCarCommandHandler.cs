using CarSharing.Modules.Fleet.Domain;
using CarSharing.Modules.Fleet.Infrastructure;
using MediatR;

namespace CarSharing.Modules.Fleet.Application.AddCar;

public sealed class AddCarCommandHandler(FleetDbContext dbContext)
    : IRequestHandler<AddCarCommand, Guid>
{
    public async Task<Guid> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        var car = new Car(Guid.NewGuid(), request.Brand, request.Model, request.RegistrationNumber);

        dbContext.Cars.Add(car);
        await dbContext.SaveChangesAsync(cancellationToken);

        return car.Id;
    }
}
