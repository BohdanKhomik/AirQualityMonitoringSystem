namespace AQIViewer.Models
{
    public class UserLocation
    {
        public int Id { get; set;}

        public int LocationPointId { get; set; }
        public LocationPoint LocationPoint { get; set; } = null!; // required reference navigation

        public int UserId { get; set; }
        public User User { get; set; } = null!; // required reference navigation
    }
}
