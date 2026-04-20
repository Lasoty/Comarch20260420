using CarSharing.Modules.Fleet.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Fleet.Application.GetCars;

public sealed class GetCarsQueryHandler(FleetDbContext dbContext)
    : IRequestHandler<GetCarsQuery, IReadOnlyList<CarDto>>
{
    public async Task<IReadOnlyList<CarDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Cars
            .AsNoTracking()
            .Select(x => new CarDto(
                x.Id,
                x.Brand,
                x.Model,
                x.RegistrationNumber,
                x.IsAvailable))
            .ToListAsync(cancellationToken);
    }
}
