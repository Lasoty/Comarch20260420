using CarSharing.Modules.Reservations.Application.CreateReservation;
using CarSharing.Modules.Reservations.Application.GetReservationSagaStates;
using CarSharing.Modules.Reservations.Application.GetReservations;
using CarSharing.Modules.Reservations.Application.StartReservationSaga;
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

    [HttpPost("process")]
    public async Task<IActionResult> StartProcess(
        [FromBody] StartReservationSagaCommand command,
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

    [HttpGet("processes")]
    public async Task<IActionResult> GetProcesses(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetReservationSagaStatesQuery(), cancellationToken);
        return Ok(result);
    }
}
