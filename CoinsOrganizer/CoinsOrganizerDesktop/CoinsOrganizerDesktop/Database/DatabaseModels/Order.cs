using System.ComponentModel.DataAnnotations;

namespace CoinsOrganizerDesktop.Database.DatabaseModels
{
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
        
        public int CoinId { get; set; }

        public virtual Coin Coin { get; set; }
    }
}
