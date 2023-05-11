using DayTraderProAPI.Core.Entities.Identity;

namespace DayTraderProAPI.Core.Entities
{
    public class OrderEntity : BaseEntity
    {
        public int OrderId { get; set; }

        public string? CoinName { get; set; }

        public decimal OrderAmount { get; set; }

        public string? OrderDirection { get; set; }

        public string? OrderType { get; set; }

        public int AppUserId { get; set; } // Foreign Key

        public AppUser AppUser { get; set; } // Navigation Property
    }
}
