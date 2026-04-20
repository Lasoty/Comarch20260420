using MediatR;

namespace CarSharing.Modules.Notifications.Application.GetNotificationLogs;

public sealed record GetNotificationLogsQuery() : IRequest<IReadOnlyList<NotificationLogDto>>;

public sealed record NotificationLogDto(
    Guid Id,
    string Message,
    DateTime CreatedAtUtc);
