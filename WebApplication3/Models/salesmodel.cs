using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class salesmodel
    {
        public class DTO: SalesDTO
        {
            public DTO()
            {
                ListOfSales = new List<SalesDTO>();       
            }
           
            public List<SalesDTO> ListOfSales { get; set; }
        }

        public class SalesDTO
        {
            public int SaleId { get; set; }
            public string ProductName { get; set; }
            public DateTime ExpireDate { get; set; }
            public int Stock { get; set; }
            public double Price { get; set; }
            public double Total { get { return Quantity* Price; } }
            public int Quantity { get; set; }
        }
        public class DAO
        {           
            public List<string> Search(string name)
            {      using(var db =  new ApplicationDbContext() )
                   {
                        return
                        db.Products.Where(p => p.ProductName.StartsWith(name)).Select(p => p.ProductName).ToList();
                    }                
            }
        }
        
    }
}