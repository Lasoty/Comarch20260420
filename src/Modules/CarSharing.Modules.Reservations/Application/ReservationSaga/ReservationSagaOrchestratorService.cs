using CarSharing.Modules.Fleet.PublicApi;
using CarSharing.Modules.Payments.PublicApi;
using CarSharing.Modules.Reservations.Contracts;
using CarSharing.Modules.Reservations.Domain;
using CarSharing.Modules.Reservations.Infrastructure;
using MediatR;

namespace CarSharing.Modules.Reservations.Application.ReservationSaga;

public sealed class ReservationSagaOrchestratorService(
    ReservationsDbContext dbContext,
    IFleetModuleApi fleetModuleApi,
    IPaymentsModuleApi paymentsModuleApi,
    IPublisher publisher)
    : IReservationSagaOrchestratorService
{
    public async Task<Guid> StartAsync(
        Guid carId,
        Guid userId,
        DateTime fromUtc,
        DateTime toUtc,
        decimal price,
        CancellationToken cancellationToken = default)
    {
        var isAvailable = await fleetModuleApi.IsCarAvailableAsync(carId, cancellationToken);

        if (!isAvailable)
        {
            throw new InvalidOperationException("Car is not available.");
        }

        var reservation = new Reservation(Guid.NewGuid(), carId, userId, fromUtc, toUtc);
        dbContext.Reservations.Add(reservation);

        var saga = new ReservationSagaState(
            Guid.NewGuid(),
            reservation.Id,
            reservation.CarId,
            reservation.UserId,
            price,
            DateTime.UtcNow);

        dbContext.ReservationSagaStates.Add(saga);
        await dbContext.SaveChangesAsync(cancellationToken);

        await fleetModuleApi.MarkCarUnavailableAsync(carId, cancellationToken);
        saga.MarkCarReserved();
        await dbContext.SaveChangesAsync(cancellationToken);

        var paymentResult = await paymentsModuleApi.AuthorizeAsync(reservation.Id, price, cancellationToken);

        if (paymentResult.IsAuthorized)
        {
            saga.MarkPaymentAuthorized();
            reservation.Confirm();

            saga.MarkCompleted();
            await dbContext.SaveChangesAsync(cancellationToken);

            await publisher.Publish(
                new ReservationConfirmed(
                    reservation.Id,
                    reservation.CarId,
                    reservation.UserId,
                    reservation.FromUtc,
                    reservation.ToUtc,
                    price,
                    DateTime.UtcNow),
                cancellationToken);

            return reservation.Id;
        }

        saga.MarkCompensating();
        await dbContext.SaveChangesAsync(cancellationToken);

        await fleetModuleApi.MarkCarAvailableAsync(carId, cancellationToken);

        reservation.Reject();
        saga.MarkRejected();

        await dbContext.SaveChangesAsync(cancellationToken);

        await publisher.Publish(
            new ReservationRejected(
                reservation.Id,
                reservation.CarId,
                reservation.UserId,
                price,
                paymentResult.Reason,
                DateTime.UtcNow),
            cancellationToken);

        return reservation.Id;
    }
}
