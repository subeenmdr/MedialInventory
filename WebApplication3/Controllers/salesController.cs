using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using PagedList;
using PagedList.Mvc;
namespace WebApplication3.Controllers
{[Authorize]
    public class salesController : Controller
    {
        // GET: sales
        public ActionResult sales()
        {
            return View();
        }

        [HttpPost]
        public ActionResult sales(salesmodel.DTO model)
        {
            TempData["SalesModel"] = model;
            using (var db = new ApplicationDbContext())
            {
                Sale sale = new Sale();
                foreach(var item in model.ListOfSales)
                {
                    sale.ProductName = item.ProductName;
                    sale.Price = item.Price;
                    sale.Quantity = item.Quantity;
                    sale.Total = item.Total;
                    sale.ExpireDate = item.ExpireDate;
                    db.Sales.Add(sale);
                    var product = db.Products.Where(x => x.ProductName == item.ProductName).FirstOrDefault();
                    product.Stock = product.Stock - item.Quantity;
                    db.SaveChanges();
                }
            }
                return RedirectToAction("PrintSales");
        }
        
        public ActionResult printsales()
        {
            salesmodel.DTO model =(salesmodel.DTO)TempData["SalesModel"];

            return View(model);

        }
        public ActionResult AddSales()
        { salesmodel.DTO model = new salesmodel.DTO();
            return PartialView("~/Views/Sales/EditorTemplates/SalesDTO.cshtml",model);
        }

        public JsonResult GetStock(string productName)
        {
            Product product = new Product();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                product = db.Products.Where(x => x.ProductName.Contains(productName)).FirstOrDefault();
            }
            var stock = product.Stock;
            var price = product.Price;
            DateTime expDate = product.ExpireDate;
            return Json(new
            {
                stock,
                price,
                expDate
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string term)
        {
            salesmodel.DAO pm = new salesmodel.DAO();
            return Json(pm.Search(term), JsonRequestBehavior.AllowGet);
        }

        
        
        public ActionResult saleslist(int ?page)
        {
            IPagedList<Sale> model;
            using (var db = new ApplicationDbContext())
            {
               model = db.Sales.ToList().ToPagedList( page ?? 1, 20);
            }
                return View(model);
        }
    }
}