using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.Identity;
using DayTraderProAPI.Core.Interfaces;
using DayTraderProAPI.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI.Application.CustomService
{
    public class WatchlistService : IWatchlistService
    {
    
        private readonly AppDbContext _dbContext;

        public WatchlistService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WatchlistEntity>> GetWatchlistItemsAsync(string AppUserId)
        {
            return await _dbContext.WatchlistEntities.Where(w => w.AppUserId == AppUserId).ToListAsync();
        }

        public async Task<WatchlistEntity> AddToWatchlistAsync(string AppUserId, WatchlistEntity watchlistItem)
        {
            watchlistItem.AppUserId = AppUserId;
            _dbContext.WatchlistEntities.Add(watchlistItem);
            await _dbContext.SaveChangesAsync();
            return watchlistItem;
        }

        public async Task RemoveFromWatchlistAsync(string AppUserId, int watchlistId)
        {
            var watchlistItem = await _dbContext.WatchlistEntities
                .SingleOrDefaultAsync(w => w.AppUserId == AppUserId && w.WatchlistId == watchlistId);

            if (watchlistItem != null)
            {
                _dbContext.WatchlistEntities.Remove(watchlistItem);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
