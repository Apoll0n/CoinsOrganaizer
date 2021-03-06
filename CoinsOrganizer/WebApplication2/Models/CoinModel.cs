﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinsOrganizer.Models
{
    public class CoinModel
    {
        public int Id { get; set; }
        public int CoinId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Link { get; set; }
        public string PolishName { get; set; }
        public string EnglishName { get; set; }
        public double ZlotyPrice { get; set; }
        public double DollarPrice { get; set; }
        public bool IsSold { get; set; }
        public bool IsInStock { get; set; }
        public string AversFotoLink { get; set; }
        public string ReversFotoLink { get; set; }
    }
}
