using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Security.Cryptography;
using Newtonsoft.Json;
using DayTraderProAPI.Core.Entities.Identity;
using DayTraderProAPI.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI.Application.CustomService
{
    public class OrderService : IOrderService
    {
  
        private readonly AppDbContext _dbContext;
        private readonly string? apiKey;
        private readonly string? secretKey;

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderService(string? apiKey, string? secretKey)
        {
            this.apiKey = apiKey;
            this.secretKey = secretKey;
        }

        // CREATE ORDER
        public async Task<OrderEntity> CreateOrderAsync(
             string AppUserId,
             string OrderType,
             decimal OrderAmount,
             string OrderDirection,
             string CoinName,
             string CBAccessKey
            )
        {
            string url = "https://api.coinbase.com/api/v3/brokerage/orders";

            string clientOrderId = Guid.NewGuid().ToString();
            string productId = $"{CoinName}-USD";
            string side = OrderType.ToUpper();
            string quoteSize = OrderAmount.ToString();
            string stopDirection = OrderDirection.ToUpper();

            string payload = $"{clientOrderId}{productId}{side}{quoteSize}{stopDirection}";

            // Create a SHA256 HMAC with the secret
            string signature = GenerateSignature(payload, secretKey);

            string timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

            using HttpClient client = new();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", CBAccessKey);
            client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", signature);
            client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", timestamp);

            var body = new
            {
                client_order_id = clientOrderId,
                product_id = productId,
                side,
                order_configuration = new
                {
                    market_market_ioc = new
                    {
                        quote_size = quoteSize
                    },

                    stop_limit_stop_limit_gtd = new
                    {
                        stop_direction = stopDirection
                    }
                }
            };

            string requestBody = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await client.PostAsync("", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            OrderEntity orderEntity = new()
            {
                OrderGuid = clientOrderId,
                OrderAmount = OrderAmount,
                OrderDirection = OrderDirection,
                CoinName = CoinName,
                OrderType = OrderType,
                AppUserId = AppUserId
            };

          

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                OrderEntity order = JsonConvert.DeserializeObject<OrderEntity>(responseBody);
                _dbContext.OrderEntities.Add(orderEntity);
                await _dbContext.SaveChangesAsync();
                return order;
            }
            else
            {
                // Handle the error response if needed
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception("Failed to create the order: " + errorMessage);
            }
        }


        // CANCEL ORDER
        public async Task<OrderEntity> CancelOrderAsync(string OrderGuid, string CBAccessKey)
        {
            string url = "https://api.coinbase.com/api/v3/brokerage/orders/batch_cancel";

            string timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            string payload = $"{OrderGuid}{timestamp}";

            // Create a SHA256 HMAC with the secret
            string signature = GenerateSignature(payload, secretKey);

            using HttpClient client = new();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", CBAccessKey);
            client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", signature);
            client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", timestamp);

            var body = new
            {
                order_ids = new[] { OrderGuid.ToString() }
            };

            string requestBody = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await client.PostAsync("", new StringContent(requestBody, Encoding.UTF8, "application/json"));



            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                // Delete the order from the database using OrderId
                var orderToDelete = _dbContext.OrderEntities.FirstOrDefault(o => o.OrderGuid == OrderGuid);
                if (orderToDelete != null)
                {
                    _dbContext.OrderEntities.Remove(orderToDelete);
                    await _dbContext.SaveChangesAsync();
                }
                OrderEntity order = JsonConvert.DeserializeObject<OrderEntity>(responseBody);
                return order;
            }
            else
            {
                // Handle the error response if needed
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception("Failed to cancel the order: " + errorMessage);
            }
        }

        public async Task<List<OrderEntity>> GetOrdersAsync(string AppUserId)
        {
            // Query DB to get orders for the specified AppUserId
            var orders = await _dbContext.OrderEntities
                .Where(o => o.AppUserId == AppUserId)
                .ToListAsync();

            return orders;
        }

        // GENERATE SIGNATURE
        static string GenerateSignature(string payload, string secretKey)
        {
            using HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(secretKey));
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
