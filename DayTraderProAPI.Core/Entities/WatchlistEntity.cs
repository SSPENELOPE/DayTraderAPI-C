using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities
{
    [Table("watchlist")]
    public class WatchlistEntity
    {

        public int WatchlistId { get; set; }

        public string? CoinName { get; set; }

        public int UserId { get; set; } // Foreign Key
    }
}
