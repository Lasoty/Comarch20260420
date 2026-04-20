using CarSharing.Modules.Fleet.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Fleet.PublicApi;

public sealed class FleetModuleApiService(FleetDbContext dbContext) : IFleetModuleApi
{
    public async Task<bool> IsCarAvailableAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        var car = await dbContext.Cars.SingleOrDefaultAsync(x => x.Id == carId, cancellationToken);
        return car is not null && car.IsAvailable;
    }

    public async Task MarkCarUnavailableAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        var car = await dbContext.Cars.SingleOrDefaultAsync(x => x.Id == carId, cancellationToken);

        if (car is null)
        {
            throw new InvalidOperationException("Car not found.");
        }

        car.MarkUnavailable();
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
