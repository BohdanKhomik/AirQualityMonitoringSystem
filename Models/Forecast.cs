namespace AQIViewer.Models
{
    public class Forecast
    {
        public int Id { get; set; }
        public int PredictedAQI { get; set; }
        public TimeSpan ForecastDate { get; set; }
        public ICollection<AQRForecast> AQRForecasts { get; } = new List<AQRForecast>();

    }
}
