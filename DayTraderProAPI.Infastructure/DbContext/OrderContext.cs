using DayTraderProAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayTraderProAPI.Infastructure.Repositories
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<OrderEntity> OrderEntities { get; set; }
    }
}
