﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CoinsOrganizerDesktop.Database.DatabaseModels;

namespace CoinsOrganizerDesktop.DataBase.DbContext
{
    public class CoinsOrganizerContext : System.Data.Entity.DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CoinsOrganizerContext(string connString) : base(connString)
        {
            //Database.Initialize();

            //Database.SetInitializer(new DropCreateDatabaseAlways<CoinsOrganizerContext>());
            this.Database.Connection.ConnectionString = connString;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>().Ignore(b => b.CoinBL);
            //base.OnModelCreating(modelBuilder);
        }
    }

    public class MigrationsContextFactory : IDbContextFactory<CoinsOrganizerContext>
    {
        public CoinsOrganizerContext Create()
        {
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            return new CoinsOrganizerContext(connectionString);
        }
    }
}
