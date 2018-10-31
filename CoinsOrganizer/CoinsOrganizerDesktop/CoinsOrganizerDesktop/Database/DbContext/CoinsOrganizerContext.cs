using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CoinsOrganizerDesktop.Database.DatabaseModels;

namespace CoinsOrganizerDesktop.DataBase.DbContext
{
    public class CoinsOrganizerContext : System.Data.Entity.DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CoinsOrganizerContext(string connString) : base(connString)
        {
            this.Database.Connection.ConnectionString = connString;
        }
    }
}
