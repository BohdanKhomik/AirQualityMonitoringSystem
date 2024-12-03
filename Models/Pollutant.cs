namespace AQIViewer.Models
{
    public class Pollutant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Measure { get; set; }

        public ICollection<PollutantLevel> PollutantLevels { get; } = new List<PollutantLevel>();
        public ICollection<PollutionRecord> PollutionRecords { get; } = new List<PollutionRecord>();
    }
}
