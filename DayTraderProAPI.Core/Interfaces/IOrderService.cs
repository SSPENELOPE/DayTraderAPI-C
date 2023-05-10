using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Interfaces
{
    public interface IOrderService
    {
        // Create Order
        Task<Order> CreateOrderAsync(
            int UserId,
            string OrderType,
            decimal OrderAmount,
            string OrderDirection,
            string CoinName
        );

        // Cancel Order
        Task<Order> CancelOrderAsync(
          int UserId,
          int OrderId
            );

        // Retrieve Users Orders
        Task<List<OrderEntity>> GetOrdersAsync(int UserId);
    }
}
