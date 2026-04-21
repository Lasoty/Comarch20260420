namespace CarSharing.Modules.Payments.PublicApi;

public interface IPaymentsModuleApi
{
    Task<PaymentAuthorizationResult> AuthorizeAsync(
        Guid reservationId,
        decimal amount,
        CancellationToken cancellationToken = default);
}

public sealed record PaymentAuthorizationResult(
    bool IsAuthorized,
    string Reason);
