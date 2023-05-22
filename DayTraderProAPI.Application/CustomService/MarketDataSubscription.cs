using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using DayTraderProAPI.Core.Interfaces;
using WebSocketSharp;
using RestSharp;
using System.Net.WebSockets;

namespace DayTraderProAPI.Application.CustomService
{
    public class MarketDataSubscription : IMarketSubscription
    {
        private const string WebSocketUrl = "wss://ws-feed.pro.coinbase.com";
        private const string BaseUrl = "https://api.coinbase.com/v2";
        private readonly string _apiKey;
        private readonly string _secretKey;

        public MarketDataSubscription(string apiKey, string secretKey)
        {
            _secretKey = secretKey;
            _apiKey = apiKey;
        }

        public async Task<decimal> GetSpotPrice(string currencyPair)
        {
            using var httpClient = new HttpClient();
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

        public void SubscribeToMarketData(Action<string> onDataReceived)
        {
            var webSocket = new ClientWebSocket();

            // Connect to the WebSocket server

            var cancellationToken = new CancellationToken();

            webSocket.ConnectAsync(new Uri(WebSocketUrl), cancellationToken).Wait();

            // Subscribe to market data

            var subscribeMessage = new
            {
                type = "subscribe",
                product_ids = new[] { "BTC-USD", "LTC-USD" },
                channels = new object[]
                {  
                    "heartbeat",
            new { name = "ticker", product_ids = new[] { "BTC-USD", "LTC-USD" } }
                }
            };

            string subscribeJson = JsonConvert.SerializeObject(subscribeMessage);

            var subscribeBuffer = Encoding.UTF8.GetBytes(subscribeJson);
            var subscribeSegment = new ArraySegment<byte>(subscribeBuffer);

            webSocket.SendAsync(subscribeSegment, WebSocketMessageType.Text, true, cancellationToken).Wait();

            // Receive market data

            var receiveBuffer = new byte[1024];
            var receiveSegment = new ArraySegment<byte>(receiveBuffer);

            while (webSocket.State == System.Net.WebSockets.WebSocketState.Open)
            {
                var receiveResult = webSocket.ReceiveAsync(receiveSegment, cancellationToken).Result;

                if (receiveResult.MessageType == WebSocketMessageType.Text)
                {
                    var receivedData = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);

                    onDataReceived(receivedData);
                }
            }

            webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken).Wait();
        }
    }}
