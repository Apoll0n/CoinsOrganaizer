using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataAccessLayer
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoinId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public double SalePrice { get; set; }
        public string TrackNumber { get; set; }
        public string OrderDetails { get; set; }
    }
}
