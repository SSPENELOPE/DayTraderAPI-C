using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities
{
    public class WatchlistItems
    {
        public WatchlistItems() 
        {

        }

        public WatchlistItems(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public List<WatchlistEntity> Items { get; set; } = new List<WatchlistEntity>();
    }
}
