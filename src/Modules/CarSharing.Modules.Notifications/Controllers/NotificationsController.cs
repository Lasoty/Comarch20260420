using CarSharing.Modules.Notifications.Application.GetNotificationLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Modules.Notifications.Controllers;

[ApiController]
[Route("api/notifications")]
public sealed class NotificationsController(ISender sender) : ControllerBase
{
    [HttpGet("logs")]
    public async Task<IActionResult> GetLogs(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetNotificationLogsQuery(), cancellationToken);
        return Ok(result);
    }
}
