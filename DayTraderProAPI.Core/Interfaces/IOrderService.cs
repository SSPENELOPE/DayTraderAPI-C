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
        Task<OrderEntity> CreateOrderAsync(
            string AppUserId,
            string OrderType,
            decimal OrderAmount,
            string OrderDirection,
            string CoinName,
            string CBAccessKey
        );

        // Cancel Order
        Task<OrderEntity> CancelOrderAsync(
          string OrderGuid,
          string CBAccessKey
            );

        // Retrieve Users Orders
        Task<List<OrderEntity>> GetOrdersAsync(string AppUserId);
    }
}
