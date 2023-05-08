using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayTraderProAPI.Dtos
{
    [Table("watchlist")]
    public class WatchlistDto
    {
        public int WatchlistId { get; set; }

        [Required]
        public string? CoinName { get; set; }

        public int UserId { get; set; } // Foreign Key

    }
}
