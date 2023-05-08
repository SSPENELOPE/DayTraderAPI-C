using DayTraderProAPI.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Interfaces
{
    public interface IStopLossService
    {
        Task<Order> CreateStopLossAync(
         string Username,
         string OrderType,
         decimal OrderAmount,
         string OrderDirection,
         string CoinName);
    }
}
