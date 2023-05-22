using DayTraderProAPI.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayTraderProAPI.Core.Entities
{
    public class OrderEntity : BaseEntity
    {
        [Key]
        public int OrderId { get; set; } // Primary Key
        public string? OrderGuid { get; set; }

        public string? CoinName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderAmount { get; set; }

        public string? OrderDirection { get; set; }

        public string? OrderType { get; set; }

        public string? AppUserId { get; set; } // Foreign Key

        public AppUser AppUser { get; set; } // Navigation Property
    }
}
