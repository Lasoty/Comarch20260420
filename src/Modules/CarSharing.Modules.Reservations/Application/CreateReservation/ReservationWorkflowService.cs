using CarSharing.Modules.Fleet.PublicApi;
using CarSharing.Modules.Reservations.Contracts;
using CarSharing.Modules.Reservations.Domain;
using CarSharing.Modules.Reservations.Infrastructure;
using MediatR;

namespace CarSharing.Modules.Reservations.Application.CreateReservation;

public interface IReservationWorkflowService
{
    Task<Guid> CreateAsync(CreateReservationCommand request, CancellationToken cancellationToken);
}

public sealed class ReservationWorkflowService(
    ReservationsDbContext dbContext,
    IFleetModuleApi fleetModuleApi,
    IPublisher publisher) : IReservationWorkflowService
{
    public async Task<Guid> CreateAsync(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var isAvailable = await fleetModuleApi.IsCarAvailableAsync(request.CarId, cancellationToken);

        if (!isAvailable)
        {
            throw new InvalidOperationException("Car is not available.");
        }

        await fleetModuleApi.MarkCarUnavailableAsync(request.CarId, cancellationToken);

        var reservation = new Reservation(
            Guid.NewGuid(),
            request.CarId,
            request.UserId,
            request.FromUtc,
            request.ToUtc);

        dbContext.Reservations.Add(reservation);
        await dbContext.SaveChangesAsync(cancellationToken);

        await publisher.Publish(
            new CarReserved(
                reservation.Id,
                reservation.CarId,
                reservation.UserId,
                reservation.FromUtc,
                reservation.ToUtc,
                DateTime.UtcNow),
            cancellationToken);

        return reservation.Id;
    }
}
