using Microsoft.AspNetCore.Identity;


namespace DayTraderProAPI.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation Properties
        public ICollection<OrderEntity> Orders { get; set; }

        public ICollection<WatchlistEntity> Watchlists { get; set; }

        public AppUser()
        {
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            Orders = new HashSet<OrderEntity>();
            Watchlists = new HashSet<WatchlistEntity>();
        }

    }
}
