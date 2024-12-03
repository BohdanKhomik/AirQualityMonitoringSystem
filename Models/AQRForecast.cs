namespace AQIViewer.Models
{
    public class AQRForecast
    {
        public int Id { get; set; }
        public int AQRId { get; set; }
        public AirQualityRecord AirQualityRecord { get; set; } = null!; // required reference navigation
        public int ForecastId { get; set; }
        public Forecast Forecast { get; set; } = null!; // required reference navigation
    }
}
