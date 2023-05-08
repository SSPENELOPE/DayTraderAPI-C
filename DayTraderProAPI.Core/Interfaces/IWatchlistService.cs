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
        Task<IEnumerable<WatchlistEntity>> GetWatchlistItemsAsync(string userId);
        Task AddToWatchlistAsync(string userId, WatchlistEntity watchlistItem);
        Task RemoveFromWatchlistAsync(string userId, int watchlistItemId);
    }
}
