using DayTraderProAPI.Core.Entities;
using DayTraderProAPI.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayTraderProAPI.Infastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<WatchlistEntity> WatchlistEntities { get; set; }
        public DbSet<OrderEntity> OrderEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.AppUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.AppUserId);

            modelBuilder.Entity<WatchlistEntity>()
                .HasOne(w => w.AppUser)
                .WithMany(u => u.Watchlists)
                .HasForeignKey(w => w.AppUserId);

            modelBuilder.Entity<WatchlistEntity>(ConfigureWatchlistEntity);
            modelBuilder.Entity<OrderEntity>(ConfigureOrderEntity);
        }

        private void ConfigureOrderEntity(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.Property(o => o.OrderAmount)
                .HasColumnType("decimal(18, 2)")
                .HasPrecision(5);

            builder.Property(e => e.OrderId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

        }

        private void ConfigureWatchlistEntity(EntityTypeBuilder<WatchlistEntity> builder)
        {
            builder.Property(e => e.WatchlistId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);
        }
    }
}
