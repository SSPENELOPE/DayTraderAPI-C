using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }

        public Order(string userName, string orderDirection, string coinName, decimal orderAmount, string orderType)
        {
            Username = userName;
            OrderDirection = orderDirection;
            CoinName = coinName;
            OrderAmount = orderAmount;
            OrderType = orderType;
        }

        public int UserId { get; set; }

        public string OrderType { get; set; }

        public string Username { get; set; }

        public string OrderDirection { get; set; }

        public string CoinName { get; set;}

        public decimal OrderAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
