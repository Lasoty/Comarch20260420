using FluentValidation;

namespace CarSharing.Modules.Reservations.Application.CreateReservation;

public sealed class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FromUtc).LessThan(x => x.ToUtc);
        RuleFor(x => x.ToUtc).GreaterThan(x => x.FromUtc);
    }
}
