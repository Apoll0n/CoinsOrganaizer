using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class Context : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CoinsGroupDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(p => p.Coin)
                .WithOne(i => i.Order)
                .HasForeignKey<Coin>(b => b.OrderForeignKey);
            //modelBuilder.Entity<Order>().ToTable("Orders");
            //modelBuilder.Entity<Coin>().ToTable("Coins");
            //modelBuilder.Entity<Order>().HasOne(p => p.CoinCost).WithOne();
        }
    }
}
