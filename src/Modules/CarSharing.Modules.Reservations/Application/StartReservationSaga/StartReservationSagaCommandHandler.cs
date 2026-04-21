using CarSharing.Modules.Reservations.Application.ReservationSaga;
using MediatR;

namespace CarSharing.Modules.Reservations.Application.StartReservationSaga;

public sealed class StartReservationSagaCommandHandler(
    IReservationSagaOrchestratorService orchestratorService)
    : IRequestHandler<StartReservationSagaCommand, Guid>
{
    public Task<Guid> Handle(StartReservationSagaCommand request, CancellationToken cancellationToken)
    {
        return orchestratorService.StartAsync(
            request.CarId,
            request.UserId,
            request.FromUtc,
            request.ToUtc,
            request.Price,
            cancellationToken);
    }
}
