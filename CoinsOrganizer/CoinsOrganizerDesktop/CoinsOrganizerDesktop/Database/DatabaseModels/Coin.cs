﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CoinsOrganizerDesktop.Database.BusinessLogic;

namespace CoinsOrganizerDesktop.Database.DatabaseModels
{
    public class Coin
    {
        private CoinBL _coinBl;

        public Coin()
        {
            _coinBl = new CoinBL(this);
        }

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

        [NotMapped]
        public CoinBL CoinBL
        {
            get
            {
                return _coinBl;
            }
        }
    }
}