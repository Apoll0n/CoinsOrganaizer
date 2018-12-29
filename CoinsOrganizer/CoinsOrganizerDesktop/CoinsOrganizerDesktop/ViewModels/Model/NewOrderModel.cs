using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsOrganizerDesktop.ViewModels.Model
{
    public class NewOrderModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public double Price { get; set; }
        public string CoinIndex { get; set; }
        public string BuyerInfo { get; set; }
        public Currency Currency { get; set; }
        public WhereSold WhereSold { get; set; }
    }
}
