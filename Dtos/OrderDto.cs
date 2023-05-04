using System.ComponentModel.DataAnnotations.Schema;

namespace DayTraderProAPI.Models
{
    [Table("order")]
    public class OrderDto
    {
        public int Id { get; set; }

        public string? OrderType { get; set; }

        public decimal? OrderAmount { get; set; }

        public string? OrderDirection { get; set; }

        public int UserId { get; set; } // Foreign Key
    }
}
