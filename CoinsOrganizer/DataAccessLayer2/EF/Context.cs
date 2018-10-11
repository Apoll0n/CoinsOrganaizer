using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer2.EF
{
    public class Context : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CoinsGroupDB2;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Coin>()
            //    .HasOne(p => p.Order)
            //    .WithOne(i => i.Coin)
            //    .HasForeignKey<Order>(b => b.CoinForeignKey);
            //modelBuilder.Entity<Order>().ToTable("Orders");
            //modelBuilder.Entity<Coin>().ToTable("Coins");
            //modelBuilder.Entity<Order>().HasOne(p => p.CoinCost).WithOne();
        }
    }
}
