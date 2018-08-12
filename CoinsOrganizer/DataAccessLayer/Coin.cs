using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Coin
    {
        public int Id { get; set; }

        [Required]
        public int CoinId { get; set; }

        [Required]
        [Display(Name = "Title Title")]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public string Link { get; set; }

        public string PolishName { get; set; }

        public string EnglishName { get; set; }

        public double ZlotyPrice { get; set; }

        public double DollarPrice { get; set; }
        public double SoldPrice { get; set; }

        public bool IsSold { get; set; }
        public bool IsInStock { get; set; }
        public string AversFotoLink { get; set; }
        public string ReversFotoLink { get; set; }
    }
}