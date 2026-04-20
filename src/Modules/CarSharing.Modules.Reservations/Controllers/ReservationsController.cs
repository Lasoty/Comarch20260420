using CarSharing.Modules.Reservations.Application.CreateReservation;
using CarSharing.Modules.Reservations.Application.GetReservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Modules.Reservations.Controllers;

[ApiController]
[Route("api/reservations")]
public sealed class ReservationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        [FromBody] CreateReservationCommand command,
        CancellationToken cancellationToken)
    {
        var reservationId = await sender.Send(command, cancellationToken);
        return Ok(new { ReservationId = reservationId });
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetReservationsQuery(), cancellationToken);
        return Ok(result);
    }
}
