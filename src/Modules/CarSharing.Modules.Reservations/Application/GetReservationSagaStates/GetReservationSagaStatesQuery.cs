using MediatR;

namespace CarSharing.Modules.Reservations.Application.GetReservationSagaStates;

public sealed record GetReservationSagaStatesQuery()
    : IRequest<IReadOnlyList<ReservationSagaStateDto>>;

public sealed record ReservationSagaStateDto(
    Guid ReservationId,
    string State,
    decimal Price,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);
