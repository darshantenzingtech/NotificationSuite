using Microsoft.AspNetCore.Mvc;
using NotificationSuite.AzureServices.Interface;

namespace NotificationSuite.Controllers;

[ApiController]
[Route("api/event")]
public class EventController : ControllerBase
{
    private readonly IEventNotificationService _eventService;

    public EventController(IEventNotificationService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost("leave-approved")]
    public async Task<IActionResult> LeaveApproved(string employeeId)
    {
        var message = "Your leave has been approved ✅";

        await _eventService.NotifyEmployeeAsync(employeeId, message);

        return Ok("Notification sent");
    }
}

