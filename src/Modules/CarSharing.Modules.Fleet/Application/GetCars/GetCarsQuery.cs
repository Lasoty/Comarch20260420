using MediatR;

namespace CarSharing.Modules.Fleet.Application.GetCars;

public sealed record GetCarsQuery() : IRequest<IReadOnlyList<CarDto>>;

public sealed record CarDto(
    Guid Id,
    string Brand,
    string Model,
    string RegistrationNumber,
    bool IsAvailable);
