namespace AQIViewer.Models
{
    public class PollutionLevel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string Color { get; set; }
        public int MinPoint { get; set; }
        public double MaxPoint { get; set; }
        public ICollection<PollutantLevel> PollutantLevels { get; } = new List<PollutantLevel>();

    }
}
