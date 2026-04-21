using MediatR;

namespace CarSharing.Modules.Reservations.Application.StartReservationSaga;

public sealed record StartReservationSagaCommand(
    Guid CarId,
    Guid UserId,
    DateTime FromUtc,
    DateTime ToUtc,
    decimal Price) : IRequest<Guid>;
