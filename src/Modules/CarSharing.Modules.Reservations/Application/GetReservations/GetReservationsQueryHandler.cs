using CarSharing.Modules.Reservations.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Reservations.Application.GetReservations;

public sealed class GetReservationsQueryHandler(ReservationsDbContext dbContext)
    : IRequestHandler<GetReservationsQuery, IReadOnlyList<ReservationDto>>
{
    public async Task<IReadOnlyList<ReservationDto>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Reservations
            .AsNoTracking()
            .Select(x => new ReservationDto(
                x.Id,
                x.CarId,
                x.UserId,
                x.FromUtc,
                x.ToUtc,
                x.Status))
            .ToListAsync(cancellationToken);
    }
}
