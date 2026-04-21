using MediatR;

namespace CarSharing.Modules.Reservations.Contracts;

public sealed record ReservationConfirmed(
    Guid ReservationId,
    Guid CarId,
    Guid UserId,
    DateTime FromUtc,
    DateTime ToUtc,
    decimal Price,
    DateTime OccurredOnUtc) : INotification;
