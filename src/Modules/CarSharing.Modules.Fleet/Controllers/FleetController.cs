using CarSharing.Modules.Fleet.Application.AddCar;
using CarSharing.Modules.Fleet.Application.GetCars;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Modules.Fleet.Controllers;

[ApiController]
[Route("api/fleet")]
public sealed class FleetController(ISender sender) : ControllerBase
{
    [HttpPost("cars")]
    public async Task<IActionResult> AddCar([FromBody] AddCarCommand command, CancellationToken cancellationToken)
    {
        var carId = await sender.Send(command, cancellationToken);
        return Ok(new { CarId = carId });
    }

    [HttpGet("cars")]
    public async Task<IActionResult> GetCars(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCarsQuery(), cancellationToken);
        return Ok(result);
    }
}
