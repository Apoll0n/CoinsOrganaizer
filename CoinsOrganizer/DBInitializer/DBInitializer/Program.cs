using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CoinsOrganizerContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");

                var blog = new Coin { Name = "Poltorak", Cost = 1, Link = "link"};
                db.Coins.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Coins
                            orderby b.Name
                    select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

    
    public class Coin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CoinId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public string Link { get; set; }

        public string PolishName { get; set; }

        public string EnglishName { get; set; }

        public double ZlotyPrice { get; set; }

        public double DollarPrice { get; set; }

        public bool IsSold { get; set; }

        public bool IsInStock { get; set; }

        public bool IsIgnored { get; set; }

        public string AversFotoLink { get; set; }

        public string ReversFotoLink { get; set; }

        public int OrderId { get; set; }

        public virtual List<Order> Order { get; set; }

    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NickName { get; set; }

        public string Email { get; set; }

        [Required]
        public double SalePrice { get; set; }

        public string TrackNumber { get; set; }

        public string OrderDetails { get; set; }

        [Required]
        public string WhereSold { get; set; }

        public string SaleCurrency { get; set; }

        public string Link { get; set; }

        public bool IsPaid { get; set; }

        public bool IsTrackedOnMarket { get; set; }

        public bool IsShipped { get; set; }

        public bool IsIgnored { get; set; }

        public int CoinId { get; set; }

        public DateTime? PlacedOnMarketDate { get; set; }

        public DateTime? SoldDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public virtual Coin Coin { get; set; }
    }

    public class CoinsOrganizerContext : System.Data.Entity.DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CoinsOrganizerContext() : base()
        {
            var connectionString = @"Data Source=DELLM3800\SQLEXPRESS;Initial Catalog=CoinsOrganizerContext;Integrated Security=True";
            this.Database.Connection.ConnectionString = connectionString;

        }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>().Ignore(b => b.CoinBL);
            //base.OnModelCreating(modelBuilder);
        }*/
    }

}
