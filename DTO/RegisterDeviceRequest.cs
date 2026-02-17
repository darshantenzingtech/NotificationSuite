namespace NotificationSuite.DTO
{
    public class RegisterDeviceRequest
    {
        public string EmployeeId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string OS { get; set; }
    }
}
