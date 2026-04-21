using CarSharing.Modules.Payments.Domain;
using CarSharing.Modules.Payments.Infrastructure;

namespace CarSharing.Modules.Payments.PublicApi;

public sealed class PaymentsModuleApiService(PaymentsDbContext dbContext) : IPaymentsModuleApi
{
    public async Task<PaymentAuthorizationResult> AuthorizeAsync(
        Guid reservationId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        var isAuthorized = amount <= 300m;

        var attempt = new PaymentAttempt(
            Guid.NewGuid(),
            reservationId,
            amount,
            isAuthorized ? PaymentStatuses.Authorized : PaymentStatuses.Rejected,
            DateTime.UtcNow);

        dbContext.PaymentAttempts.Add(attempt);
        await dbContext.SaveChangesAsync(cancellationToken);

        return isAuthorized
            ? new PaymentAuthorizationResult(true, "Payment authorized.")
            : new PaymentAuthorizationResult(false, "Payment rejected by business rule.");
    }
}
