using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Application.CustomService
{
    public class OrderService : IOrderService
    {
     public async Task<Order> CreateOrderAsync(
            int UserId,
            string OrderType,
            decimal OrderAmount,
            string OrderDirection,
            string CoinName
            )
        {
            //Execute 3rd Party API here
        }

     public async Task<Order> CancelOrderAsync(
            int UserId,
            int OrderId
            )
        {
            // Execute 3rd party API here
        }

     public async Task<List<OrderEntity>> GetOrdersAsync(int UserId)
        {
            // Query DB here
        }
    }
}
