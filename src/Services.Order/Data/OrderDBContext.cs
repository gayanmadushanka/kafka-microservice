using Microsoft.EntityFrameworkCore;

namespace Services.Order.Data
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        {
        }

        public DbSet<OrderData> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("order");
            modelBuilder.Entity<OrderData>().ToTable("orders");
        }
    }
}