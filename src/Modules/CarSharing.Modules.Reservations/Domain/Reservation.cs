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
        Status = ReservationStatuses.Pending;
    }

    public void Confirm()
    {
        if (Status != ReservationStatuses.Pending)
        {
            throw new InvalidOperationException("Only pending reservation can be confirmed.");
        }

        Status = ReservationStatuses.Confirmed;
    }

    public void Reject()
    {
        if (Status != ReservationStatuses.Pending)
        {
            throw new InvalidOperationException("Only pending reservation can be rejected.");
        }

        Status = ReservationStatuses.Rejected;
    }
}

public static class ReservationStatuses
{
    public const string Pending = "Pending";
    public const string Confirmed = "Confirmed";
    public const string Rejected = "Rejected";
}
