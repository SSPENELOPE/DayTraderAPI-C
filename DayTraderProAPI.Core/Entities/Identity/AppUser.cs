using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public int UserId { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; } = DateTime.UtcNow;


        //Navigation Properties
        public ICollection<OrderEntity> Orders { get; set; }

        public ICollection<WatchlistEntity> Watchlists { get; set; }

        public AppUser()
        {
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            Orders = new HashSet<OrderEntity>();
            Watchlists = new HashSet<WatchlistEntity>();
        }

    }
}
