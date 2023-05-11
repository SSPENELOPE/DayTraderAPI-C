using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using DayTraderProAPI.Core.Interfaces;
using WebSocketSharp;
using RestSharp;

namespace DayTraderProAPI.Application.CustomService
{
    public class MarketDataSubscription : IMarketSubscription
    {
        private const string WebSocketUrl = "wss://ws-feed.pro.coinbase.com";
        private const string BaseUrl = "https://api.coinbase.com/v2";
        private readonly string _apiKey;

        public MarketDataSubscription(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<decimal> GetSpotPrice(string currencyPair)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var response = await httpClient.GetAsync($"{BaseUrl}/prices/{currencyPair}/spot");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic? data = null;
                    if (content != null)
                    {
                        data = JsonConvert.DeserializeObject(content);
                    }
                        decimal spotPrice = data.data.amount;
                        return spotPrice;
                }
                else
                {
                    throw new Exception("An error occurred: " + response.StatusCode);
                }
            }
        
        }

        public void SubscribeToMarketData(Action<string> onDataReceived)
        {
            var webSocket = new WebSocket(WebSocketUrl);

            webSocket.OnMessage += (sender, e) =>
            {
                onDataReceived(e.Data);
            };

            webSocket.OnError += (sender, e) =>
            {
                Console.WriteLine("WebSocket error: " + e.Message);
            };

            webSocket.OnClose += (sender, e) =>
            {
                Console.WriteLine("WebSocket closed.");
            };

            webSocket.Connect();

            var subscribeMessage = new
            {
                type = "subscribe",
                product_ids = new[] { "BTC-USD", "LTC-USD" },
                channels = new object[]
                {
                "level2",
                "heartbeat",
                new { name = "ticker", product_ids = new[] { "BTC-USD", "LTC-USD" } }
                }
            };

            string subscribeJson = JsonConvert.SerializeObject(subscribeMessage);
            webSocket.Send(subscribeJson);
        }
    }
}
