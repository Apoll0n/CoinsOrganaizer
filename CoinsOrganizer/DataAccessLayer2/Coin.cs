using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer2
{
    public class Coin
    {
        [Key]
        public int Id { get; set; }
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

        public bool IsSold { get; set; }
        public bool IsInStock { get; set; }
        public string AversFotoLink { get; set; }
        public string ReversFotoLink { get; set; }

        //public Order Order { get; set; }
    }
}