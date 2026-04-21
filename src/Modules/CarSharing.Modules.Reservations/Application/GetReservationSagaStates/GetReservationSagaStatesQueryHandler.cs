using CarSharing.Modules.Reservations.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Reservations.Application.GetReservationSagaStates;

public sealed class GetReservationSagaStatesQueryHandler(ReservationsDbContext dbContext)
    : IRequestHandler<GetReservationSagaStatesQuery, IReadOnlyList<ReservationSagaStateDto>>
{
    public async Task<IReadOnlyList<ReservationSagaStateDto>> Handle(
        GetReservationSagaStatesQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.ReservationSagaStates
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new ReservationSagaStateDto(
                x.ReservationId,
                x.State,
                x.Price,
                x.CreatedAtUtc,
                x.UpdatedAtUtc))
            .ToListAsync(cancellationToken);
    }
}
