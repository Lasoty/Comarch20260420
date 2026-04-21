namespace CarSharing.Modules.Fleet.PublicApi;

public interface IFleetModuleApi
{
    Task<bool> IsCarAvailableAsync(Guid carId, CancellationToken cancellationToken = default);
    Task MarkCarUnavailableAsync(Guid carId, CancellationToken cancellationToken = default);
    Task MarkCarAvailableAsync(Guid carId, CancellationToken cancellationToken = default);
}
