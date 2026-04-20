namespace CarSharing.Modules.Notifications.Domain;

public sealed class NotificationLog
{
    public Guid Id { get; private set; }
    public string Message { get; private set; } = default!;
    public DateTime CreatedAtUtc { get; private set; }

    private NotificationLog() { }

    public NotificationLog(Guid id, string message, DateTime createdAtUtc)
    {
        Id = id;
        Message = message;
        CreatedAtUtc = createdAtUtc;
    }
}
