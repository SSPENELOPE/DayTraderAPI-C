using DayTraderProAPI.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayTraderProAPI.Core.Entities
{
    public class WatchlistEntity 
    {
        [Key]
        public int WatchlistId { get; set; }

        public string? CoinName { get; set; }
    
        public string? AppUserId { get; set; } // Foreign Key

        public AppUser AppUser { get; set; } // Navigation Property
    }
}
