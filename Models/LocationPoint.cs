namespace AQIViewer.Models
{
    public class LocationPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
        public ICollection<AirQualityRecord> AirQualityRecords { get; } = new List<AirQualityRecord>();
        public ICollection<UserLocation> UserLocations { get; } = new List<UserLocation>();
    }
}
