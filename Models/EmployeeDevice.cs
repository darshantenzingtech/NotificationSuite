namespace NotificationSuite.Models
{
    public class EmployeeDevice
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string OS { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
