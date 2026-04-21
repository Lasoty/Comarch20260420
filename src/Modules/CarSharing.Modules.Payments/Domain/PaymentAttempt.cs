namespace CarSharing.Modules.Payments.Domain;

public sealed class PaymentAttempt
{
    public Guid Id { get; private set; }
    public Guid ReservationId { get; private set; }
    public decimal Amount { get; private set; }
    public string Status { get; private set; } = default!;
    public DateTime CreatedAtUtc { get; private set; }

    private PaymentAttempt() { }

    public PaymentAttempt(Guid id, Guid reservationId, decimal amount, string status, DateTime createdAtUtc)
    {
        Id = id;
        ReservationId = reservationId;
        Amount = amount;
        Status = status;
        CreatedAtUtc = createdAtUtc;
    }
}

public static class PaymentStatuses
{
    public const string Authorized = "Authorized";
    public const string Rejected = "Rejected";
}
