using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DayTraderProAPI.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Infastructure.Identity
{
    public class IdentityContext : IdentityDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

    }
}
