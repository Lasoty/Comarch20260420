using MediatR;

namespace CarSharing.Modules.Reservations.Contracts;

public sealed record ReservationRejected(
    Guid ReservationId,
    Guid CarId,
    Guid UserId,
    decimal Price,
    string Reason,
    DateTime OccurredOnUtc) : INotification;
