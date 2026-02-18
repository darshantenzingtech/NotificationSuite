using Microsoft.EntityFrameworkCore;
using NotificationSuite.AzureServices.Interface;

namespace NotificationSuite.AzureServices.Service;

public class EventNotificationService : IEventNotificationService
{
    private readonly AppDbContext _context;
    private readonly IAzureNotificationService _azureService;

    public EventNotificationService(AppDbContext context, IAzureNotificationService azureService)
    {
        _context = context;
        _azureService = azureService;
    }

    public async Task NotifyEmployeeAsync(string employeeId, string message)
    {
        var devices = await _context.EmployeeDevices
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();

        foreach (var device in devices)
        {
            await _azureService.SendToDeviceAsync(
                device.DeviceToken,
                message
            );
        }
    }
}

