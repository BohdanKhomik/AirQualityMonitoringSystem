namespace AQIViewer.Models
{
    public class UserAlert
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } = null!; // required reference navigation

        public int AlertId { get; set; }
        public Alert Alert { get; set; } = null!; // required reference navigation
    }
}
