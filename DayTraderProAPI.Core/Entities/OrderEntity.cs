using DayTraderProAPI.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace DayTraderProAPI.Core.Entities
{
    public class OrderEntity : BaseEntity
    {
        [Key]
        public int OrderId { get; set; } // Primary Key
        public string? OrderGuid { get; set; }

        public string? CoinName { get; set; }

        public decimal OrderAmount { get; set; }

        public string? OrderDirection { get; set; }

        public string? OrderType { get; set; }

        public string? AppUserId { get; set; } // Foreign Key

        public AppUser AppUser { get; set; } // Navigation Property
    }
}
