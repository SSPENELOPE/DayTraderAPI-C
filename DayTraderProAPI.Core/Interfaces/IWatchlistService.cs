using DayTraderProAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Interfaces
{
    public interface IWatchlistService
    {
        Task<List<WatchlistEntity>> GetWatchlistItemsAsync(int userId);
        Task<WatchlistEntity> AddToWatchlistAsync(int userId, WatchlistEntity watchlistItem);
        Task RemoveFromWatchlistAsync(int userId, int WatchlistId);
    }
}
