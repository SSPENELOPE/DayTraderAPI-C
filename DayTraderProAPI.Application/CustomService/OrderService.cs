using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

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

            var client = new RestClient("https://api.coinbase.com/api/v3/brokerage/orders")
            {
                Timeout = -0b1
            }

            var request = new RestRequest("POST");
            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                client_order_id = Guid.NewGuid().ToString(),
                product_id = CoinName + "-USD",
                side = OrderType.ToUpper(),
                order_configuration = new
                {
                    market_market_ioc = new
                    {
                        quote_size = OrderAmount.ToString()
                    },

                    stop_limit_stop_limit_gtd = new
                    {
                        stop_direction = OrderDirection.ToUpper()
                    }
                }
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync<Order>(request);
            if (response.IsSuccessful)
            {
                Order order = response.Data;
                return order;
            }
            else
            {
                // Handle the error response if needed
                throw new Exception("Failed to create the order: " + response.ErrorMessage);
            }

        }  

     public async Task<Order> CancelOrderAsync(int UserId,int OrderId)
        {
            // Execute 3rd party API here
        }

     public async Task<List<OrderEntity>> GetOrdersAsync(int UserId)
        {
            // Query DB here
        }
    }
}
