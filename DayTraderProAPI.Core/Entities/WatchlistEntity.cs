using DayTraderProAPI.Core.Entities.Identity;

namespace DayTraderProAPI.Core.Entities
{
    public class WatchlistEntity 
    {

        public int WatchlistId { get; set; }

        public string? CoinName { get; set; }

        public int AppUserId { get; set; } // Foreign Key

        public AppUser AppUser { get; set; } // Navigation Property
    }
}
