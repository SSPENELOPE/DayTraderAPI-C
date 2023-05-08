using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Infastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI.Application.CustomService
{
    public class WatchlistService
    {
        private readonly IWatchlistService _watchListService;
        private readonly WatchlistContext _watchlistContext;

        public WatchlistService(IWatchlistService watchlistService, WatchlistContext watchlistContext)
        {
            _watchListService = watchlistService;
            _watchlistContext = watchlistContext;
        }

        public async Task<List<WatchlistEntity>> GetWatchlistItemsAsync(int userId)
        {
            return await _watchlistContext.WatchlistEntities.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<WatchlistEntity> AddWatchlistItemAsync(WatchlistEntity watchlistItem)
        {
            _watchlistContext.WatchlistEntities.Add(watchlistItem);
            await _watchlistContext.SaveChangesAsync();
            return watchlistItem;
        }

        public async Task RemoveWatchlistItemAsync(WatchlistEntity watchlistItem)
        {
            _watchlistContext.WatchlistEntities.Remove(watchlistItem);
            await _watchlistContext.SaveChangesAsync();
        }
    }
}
