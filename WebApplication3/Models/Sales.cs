using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public int Quantity { get; set; }
    }
}