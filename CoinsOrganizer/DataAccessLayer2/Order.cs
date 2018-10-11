using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer2
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
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
        //public int CoinForeignKey { get; set; }

        //[ForeignKey("CoinForeignKey")]
        //public Coin Coin { get; set; }
    }
}
