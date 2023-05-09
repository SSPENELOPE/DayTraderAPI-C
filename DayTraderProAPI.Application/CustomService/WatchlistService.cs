using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI.Application.CustomService
{
    public class WatchlistService : IWatchlistService
    {
    
        private readonly WatchlistContext _watchlistContext;

        public WatchlistService(WatchlistContext watchlistContext)
        {
            _watchlistContext = watchlistContext;
        }

        public async Task<List<WatchlistEntity>> GetWatchlistItemsAsync(int userId)
        {
            return await _watchlistContext.WatchlistEntities.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<WatchlistEntity> AddToWatchlistAsync(int userId, WatchlistEntity watchlistItem)
        {
            watchlistItem.UserId = userId;
            _watchlistContext.WatchlistEntities.Add(watchlistItem);
            await _watchlistContext.SaveChangesAsync();
            return watchlistItem;
        }

        public async Task RemoveFromWatchlistAsync(int userId, int watchlistId)
        {
            var watchlistItem = await _watchlistContext.WatchlistEntities
                .SingleOrDefaultAsync(w => w.UserId == userId && w.WatchlistId == watchlistId);

            if (watchlistItem != null)
            {
                _watchlistContext.WatchlistEntities.Remove(watchlistItem);
                await _watchlistContext.SaveChangesAsync();
            }
        }
    }
}
