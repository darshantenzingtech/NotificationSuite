namespace NotificationSuite.AzureServices.Service;

using Microsoft.Azure.NotificationHubs;
using NotificationSuite.AzureServices.Interface;

public class AzureNotificationService : IAzureNotificationService
{
    private readonly NotificationHubClient _hub;

    public AzureNotificationService(IConfiguration config)
    {
        _hub = NotificationHubClient.CreateClientFromConnectionString(
            config["AzureNotificationHub:ConnectionString"],
            config["AzureNotificationHub:HubName"]
        );
    }

    // Register / Update device in Hub
    public async Task RegisterDeviceAsync(string deviceId, string token, string os)
    {
        var installation = new Installation
        {
            InstallationId = deviceId,
            PushChannel = token,
            Platform = os.ToLower() == "ios"
                ? NotificationPlatform.Apns
                : NotificationPlatform.Fcm
        };

        await _hub.CreateOrUpdateInstallationAsync(installation);
    }

    // Send notification to specific device
    public async Task SendToDeviceAsync(string deviceToken, string message)
    {
        var payload = @"{
            ""notification"": {
                ""title"": ""Employee App"",
                ""body"": """ + message + @"""
            }
        }";

        await _hub.SendFcmNativeNotificationAsync(payload, deviceToken);
    }
}

