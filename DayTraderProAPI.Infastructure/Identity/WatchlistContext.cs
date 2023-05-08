using DayTraderProAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Infastructure.Identity
{
    public class WatchlistContext : DbContext
    {
        public WatchlistContext(DbContextOptions<WatchlistContext> options) : base(options)
        {

        }

        public DbSet<WatchlistEntity> WatchlistEntities { get; set; }
    }
}
