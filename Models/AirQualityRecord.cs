namespace AQIViewer.Models
{
    public class AirQualityRecord
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int AQI { get; set; }

        public ICollection<PollutionRecord> PollutionRecords { get; } = new List<PollutionRecord>();

        public ICollection<Alert> Alerts { get; } = new List<Alert>();

        public ICollection<AQRForecast> AQRForecasts { get; } = new List<AQRForecast>();

        public int LocationPointId { get; set; }
        public LocationPoint LocationPoint { get; set; } = null!; // required reference navigation
    }
}
