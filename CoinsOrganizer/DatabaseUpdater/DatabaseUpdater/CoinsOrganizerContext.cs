using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater
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

    public class MigrationsContextFactory : IDbContextFactory<CoinsOrganizerContext>
    {
        public CoinsOrganizerContext Create()
        {
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            return new CoinsOrganizerContext(connectionString);
        }
    }
}
