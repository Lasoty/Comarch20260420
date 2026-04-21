using CarSharing.Modules.Notifications.Domain;
using CarSharing.Modules.Notifications.Infrastructure;
using CarSharing.Modules.Reservations.Contracts;
using MediatR;

namespace CarSharing.Modules.Notifications.Application;

public sealed class ReservationRejectedNotificationHandler(NotificationsDbContext dbContext)
    : INotificationHandler<ReservationRejected>
{
    public async Task Handle(ReservationRejected notification, CancellationToken cancellationToken)
    {
        var message =
            $"Reservation {notification.ReservationId} rejected. Reason: {notification.Reason}.";

        dbContext.NotificationLogs.Add(
            new NotificationLog(Guid.NewGuid(), message, DateTime.UtcNow));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
