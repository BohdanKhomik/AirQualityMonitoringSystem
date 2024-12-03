namespace AQIViewer.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsAcknowleged { get; set; }
        public TimeSpan CreatedAt { get; set; }
        public int AQRId { get; set; }
        public AirQualityRecord AirQualityRecord { get; set; } = null!; // required reference navigation

        public ICollection<UserAlert> UserAlerts { get; } = new List<UserAlert>();

    }
}
