namespace CarSharing.Modules.Reservations.Domain;

public sealed class ReservationSagaState
{
    public Guid Id { get; private set; }
    public Guid ReservationId { get; private set; }
    public Guid CarId { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Price { get; private set; }
    public string State { get; private set; } = default!;
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }

    private ReservationSagaState() { }

    public ReservationSagaState(
        Guid id,
        Guid reservationId,
        Guid carId,
        Guid userId,
        decimal price,
        DateTime createdAtUtc)
    {
        Id = id;
        ReservationId = reservationId;
        CarId = carId;
        UserId = userId;
        Price = price;
        CreatedAtUtc = createdAtUtc;
        State = ReservationSagaStates.Started;
    }

    public void MarkCarReserved()
    {
        State = ReservationSagaStates.CarReserved;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkPaymentAuthorized()
    {
        State = ReservationSagaStates.PaymentAuthorized;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkCompleted()
    {
        State = ReservationSagaStates.Completed;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkCompensating()
    {
        State = ReservationSagaStates.Compensating;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkRejected()
    {
        State = ReservationSagaStates.Rejected;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}

public static class ReservationSagaStates
{
    public const string Started = "Started";
    public const string CarReserved = "CarReserved";
    public const string PaymentAuthorized = "PaymentAuthorized";
    public const string Completed = "Completed";
    public const string Compensating = "Compensating";
    public const string Rejected = "Rejected";
}
