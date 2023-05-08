using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities
{
    public class OrderEntity : BaseEntity
    {
        public int OrderId { get; set; }

        public string? CoinName { get; set; }

        public decimal OrderAmount { get; set; }

        public string? OrderDirection { get; set; }

        public string? OrderType { get; set; }

        public int UserId { get; set; } // Foreign Key
    }
}
