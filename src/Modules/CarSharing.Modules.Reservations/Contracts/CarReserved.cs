using MediatR;

namespace CarSharing.Modules.Reservations.Contracts;

public sealed record CarReserved(
    Guid ReservationId,
    Guid CarId,
    Guid UserId,
    DateTime FromUtc,
    DateTime ToUtc,
    DateTime OccurredOnUtc) : INotification;
