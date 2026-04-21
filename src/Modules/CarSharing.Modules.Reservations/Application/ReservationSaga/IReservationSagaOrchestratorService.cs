namespace CarSharing.Modules.Reservations.Application.ReservationSaga;

public interface IReservationSagaOrchestratorService
{
    Task<Guid> StartAsync(
        Guid carId,
        Guid userId,
        DateTime fromUtc,
        DateTime toUtc,
        decimal price,
        CancellationToken cancellationToken = default);
}
