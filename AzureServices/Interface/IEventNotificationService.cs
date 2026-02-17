namespace NotificationSuite.AzureServices.Interface
{
    public interface IEventNotificationService
    {
        Task NotifyEmployeeAsync(string employeeId, string message);
    }
}
