using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Product
    {   
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Medicine name is Required")]
        public string ProductName { get; set; }
      
        public double Price { get; set; }
       public double SalesPrice { get; set; }
        public DateTime MfgDate  {get; set; }

        public DateTime ExpireDate { get; set; }
        public string BatchNo { get; set; }
        public int Stock { get; set; }

        
    }
}