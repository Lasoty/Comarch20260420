using CarSharing.Modules.Notifications.Domain;
using CarSharing.Modules.Notifications.Infrastructure;
using CarSharing.Modules.Reservations.Contracts;
using MediatR;

namespace CarSharing.Modules.Notifications.Application;

public sealed class CarReservedNotificationHandler(NotificationsDbContext dbContext)
    : INotificationHandler<CarReserved>
{
    public async Task Handle(CarReserved notification, CancellationToken cancellationToken)
    {
        var message =
            $"Reservation {notification.ReservationId} created for car {notification.CarId} and user {notification.UserId}.";

        dbContext.NotificationLogs.Add(
            new NotificationLog(Guid.NewGuid(), message, DateTime.UtcNow));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
