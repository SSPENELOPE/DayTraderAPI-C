using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities
{
    [Table("Order")]
    public class OrderEntity
    {
        public int Id { get; set; }

        public string? OrderType { get; set; }

        public decimal? OrderAmount { get; set; }

        public string? OrderDirection { get; set; }

        public int UserId { get; set; } // Foreign Key
    }
}
