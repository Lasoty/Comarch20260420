using MediatR;

namespace CarSharing.Modules.Reservations.Application.GetReservations;

public sealed record GetReservationsQuery() : IRequest<IReadOnlyList<ReservationDto>>;

public sealed record ReservationDto(
    Guid Id,
    Guid CarId,
    Guid UserId,
    DateTime FromUtc,
    DateTime ToUtc,
    string Status);
