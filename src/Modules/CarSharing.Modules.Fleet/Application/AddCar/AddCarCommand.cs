using MediatR;

namespace CarSharing.Modules.Fleet.Application.AddCar;

public sealed record AddCarCommand(
    string Brand,
    string Model,
    string RegistrationNumber) : IRequest<Guid>;
