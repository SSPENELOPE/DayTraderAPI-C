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
        Task<List<WatchlistEntity>> GetWatchlistItemsAsync(string AppUserId);
        Task<WatchlistEntity> AddToWatchlistAsync(string AppUserId, WatchlistEntity watchlistItem);
        Task RemoveFromWatchlistAsync(string AppUserId, int WatchlistId);
    }
}
