using MediatR;

namespace CarSharing.Modules.Reservations.Application.CreateReservation;

public sealed record CreateReservationCommand(
    Guid CarId,
    Guid UserId,
    DateTime FromUtc,
    DateTime ToUtc) : IRequest<Guid>;
