using DayTraderProAPI.Application.CustomService;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocketSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace DayTraderProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketDataController : Controller
    {
        private readonly IOptions<CBApiKeyDto> _keyDto;
        private static IMarketSubscription _marketSubscription;
        private static readonly List<string> _marketDataQueue = new(); // Collection to store received market data

        public MarketDataController(IMarketSubscription marketSubscription, IOptions<CBApiKeyDto> keyDto) 
        {
            _marketSubscription = marketSubscription;
            _keyDto = keyDto;
        }


        // User Can Search for specific coin here with Coin Identifier
        [HttpGet(nameof(GetSpotPrice))]
        public async Task<ActionResult<decimal>> GetSpotPrice([FromQuery] string _currencyPair)
        {
            decimal spotPrice = await _marketSubscription.GetSpotPrice(_currencyPair);
            return Ok(spotPrice);
        }

        // Subscribe and return real time market data
        [HttpPost(nameof(SubscribeToMarketData))]
        public IActionResult SubscribeToMarketData() 
        {
            _marketSubscription.SubscribeToMarketData(OnMarketDataReceived);

            return Ok("Subscripton Successful"); 
        }

        [HttpGet(nameof(GetMarketData))]
        public IActionResult GetMarketData()
        {
            return Ok(_marketDataQueue);
        }

        private void OnMarketDataReceived(string data)
        {
            Console.WriteLine("Received market data: " + data);
            _marketDataQueue.Add(data); // Store the received market data in the collection
        }
    }
}
