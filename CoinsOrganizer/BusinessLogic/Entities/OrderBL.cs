using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Entities
{
    public class OrderBL
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public double SalePrice { get; set; }
        public string TrackNumber { get; set; }
        public string OrderDetails { get; set; }
        public string WhereSold { get; set; }
        public string SaleCurrency { get; set; }
        public string Link { get; set; }
        public bool IsPaid { get; set; }
    }
}
