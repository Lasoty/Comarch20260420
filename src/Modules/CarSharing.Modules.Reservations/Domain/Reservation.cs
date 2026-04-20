namespace CarSharing.Modules.Reservations.Domain;

public sealed class Reservation
{
    public Guid Id { get; private set; }
    public Guid CarId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime FromUtc { get; private set; }
    public DateTime ToUtc { get; private set; }
    public string Status { get; private set; } = default!;

    private Reservation() { }

    public Reservation(Guid id, Guid carId, Guid userId, DateTime fromUtc, DateTime toUtc)
    {
        Id = id;
        CarId = carId;
        UserId = userId;
        FromUtc = fromUtc;
        ToUtc = toUtc;
        Status = "Confirmed";
    }
}
