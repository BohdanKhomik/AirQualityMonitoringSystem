using Microsoft.AspNetCore.Identity;

namespace AQIViewer.Models
{
    public class User : IdentityUser
    {
        public bool ReceiveAlerts { get; set; }
        public ICollection<UserLocation> UserLocations { get; } = new List<UserLocation>();
        public ICollection<UserAlert> UserAlerts { get; } = new List<UserAlert>();

    }
}
