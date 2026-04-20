using CarSharing.Modules.Notifications.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Modules.Notifications.Application.GetNotificationLogs;

public sealed class GetNotificationLogsQueryHandler(NotificationsDbContext dbContext)
    : IRequestHandler<GetNotificationLogsQuery, IReadOnlyList<NotificationLogDto>>
{
    public async Task<IReadOnlyList<NotificationLogDto>> Handle(GetNotificationLogsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.NotificationLogs
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new NotificationLogDto(x.Id, x.Message, x.CreatedAtUtc))
            .ToListAsync(cancellationToken);
    }
}
