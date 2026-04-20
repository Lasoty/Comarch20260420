using FluentValidation;

namespace CarSharing.Modules.Fleet.Application.AddCar;

public sealed class AddCarCommandValidator : AbstractValidator<AddCarCommand>
{
    public AddCarCommandValidator()
    {
        RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Model).NotEmpty().MaximumLength(100);
        RuleFor(x => x.RegistrationNumber).NotEmpty().MaximumLength(30);
    }
}
