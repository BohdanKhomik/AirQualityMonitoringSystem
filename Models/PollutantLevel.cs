namespace AQIViewer.Models
{
    public class PollutantLevel
    {
        public int Id { get; set; }
        public double MinConcentration { get; set; }
        public double MaxConcentration { get; set; }

        public int PollutionLevelId { get; set; }
        public PollutionLevel PollutionLevel { get; set; } = null!; // required reference navigation

        public int PollutantId { get; set; }
        public Pollutant Pollutant { get; set; } = null!;  // required reference navigation

        public string LevelRecomendation { get; set; }
    } 
}
