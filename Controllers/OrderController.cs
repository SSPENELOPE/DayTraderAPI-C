using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.Identity;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Infastructure.Data;
using DayTraderProAPI.Infastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private static IOrderService _orderService;
        private readonly IdentityContext _dbContext;

        public OrderController(IOrderService orderService, IdentityContext dbContext)
        {
            _dbContext = dbContext;
            _orderService = orderService;
        }

        // Create Order
        [HttpPost(nameof(CreateOrder))]
        public async Task<ActionResult> CreateOrder([FromQuery]
             string AppUserId,
             string OrderType,
             decimal OrderAmount,
             string OrderDirection,
             string CoinName)
        {
            try
            {

                var appUser = await _dbContext.AppUsers.FindAsync(AppUserId);
                if (appUser == null)
                {
                    return NotFound($"User with AppUserId {AppUserId} not found.");
                }

                // Get the CBAccessKey from the user's record
                string CBAccessKey = appUser.CBAccessKey;

                await _orderService.CreateOrderAsync(AppUserId, OrderType, OrderAmount, OrderDirection, CoinName, CBAccessKey);
                return Ok(_orderService);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Cancel Order
        [HttpPost(nameof(CancelOrder))]
        public async Task<ActionResult> CancelOrder([FromQuery] string OrderGuid, int AppUserId)
        {
            try
            {
                var appUser = await _dbContext.AppUsers.FindAsync(AppUserId);
                if (appUser == null)
                {
                    return NotFound($"User with AppUserId {AppUserId} not found.");
                }

                // Get the CBAccessKey from the user's record
                string CBAccessKey = appUser.CBAccessKey;

                await _orderService.CancelOrderAsync(OrderGuid, CBAccessKey);
                return Ok(_orderService);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Users Orders
        [HttpGet(nameof(GetOrders))]
        public async Task<ActionResult<List<OrderEntity>>> GetOrders([FromQuery] string AppUserId)
        {
            try
            {
                var orders = await _orderService.GetOrdersAsync(AppUserId);
                return Ok(orders);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured: {ex.Message}");
            }
        }
    }
}
