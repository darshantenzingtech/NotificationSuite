using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationSuite.AzureServices.Service;
using NotificationSuite.DTO;
using NotificationSuite.Models;

namespace NotificationSuite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AzureNotificationService _notificationService;

        public DeviceController(
            AppDbContext context,
            AzureNotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterDevice(RegisterDeviceRequest request)
        {
            var existing = await _context.EmployeeDevices
                .FirstOrDefaultAsync(x =>
                    x.DeviceId == request.DeviceId &&
                    x.EmployeeId == request.EmployeeId);

            if (existing == null)
            {
                var device = new EmployeeDevice
                {
                    EmployeeId = request.EmployeeId,
                    DeviceId = request.DeviceId,
                    DeviceToken = request.DeviceToken,
                    OS = request.OS,
                    CreatedDate = DateTime.UtcNow
                };

                _context.EmployeeDevices.Add(device);
            }
            else
            {
                existing.DeviceToken = request.DeviceToken;
                existing.UpdatedDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            // Register in Azure Hub
            await _notificationService.RegisterDeviceAsync(
                request.DeviceId,
                request.DeviceToken,
                request.OS
            );

            return Ok("Device registered successfully");
        }
    }
}
