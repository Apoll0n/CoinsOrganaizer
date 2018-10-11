using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataAccessLayer
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string Name { get; set; }
        public int CoinForeignKey { get; set; }
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
        [ForeignKey("CoinForeignKey")]
        public Coin Coin { get; set; }
    }
}
