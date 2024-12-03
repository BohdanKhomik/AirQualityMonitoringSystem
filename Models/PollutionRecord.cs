namespace AQIViewer.Models
{
    public class PollutionRecord
    {
        public int Id { get; set; }
        public double Concentration { get; set; }
        public int PollutantId { get; set; }
        public Pollutant Pollutant { get; set; } = null!;  // required reference navigation
        public int AQRId { get; set; }
        public AirQualityRecord AirQualityRecord { get; set; } = null!; // required reference navigation

    }
}
