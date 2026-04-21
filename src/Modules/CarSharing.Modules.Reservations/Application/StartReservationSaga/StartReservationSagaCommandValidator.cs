using FluentValidation;

namespace CarSharing.Modules.Reservations.Application.StartReservationSaga;

public sealed class StartReservationSagaCommandValidator : AbstractValidator<StartReservationSagaCommand>
{
    public StartReservationSagaCommandValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.FromUtc).LessThan(x => x.ToUtc);
    }
}
