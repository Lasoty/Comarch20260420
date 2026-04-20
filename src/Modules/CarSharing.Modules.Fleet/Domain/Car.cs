namespace CarSharing.Modules.Fleet.Domain;

public sealed class Car
{
    public Guid Id { get; private set; }
    public string Brand { get; private set; } = default!;
    public string Model { get; private set; } = default!;
    public string RegistrationNumber { get; private set; } = default!;
    public bool IsAvailable { get; private set; }

    private Car() { }

    public Car(Guid id, string brand, string model, string registrationNumber)
    {
        Id = id;
        Brand = brand;
        Model = model;
        RegistrationNumber = registrationNumber;
        IsAvailable = true;
    }

    public void MarkUnavailable()
    {
        if (!IsAvailable)
        {
            throw new InvalidOperationException("Car is already unavailable.");
        }

        IsAvailable = false;
    }

    public void MarkAvailable()
    {
        if (IsAvailable)
        {
            throw new InvalidOperationException("Car is already available.");
        }

        IsAvailable = true;
    }
}
