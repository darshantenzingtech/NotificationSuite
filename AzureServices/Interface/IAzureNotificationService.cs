namespace NotificationSuite.AzureServices.Interface
{
    public interface IAzureNotificationService
    {
        Task RegisterDeviceAsync(string deviceId, string token, string os);
    }
}
