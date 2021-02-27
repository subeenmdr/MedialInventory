using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using  PagedList;
using  PagedList.Mvc;
namespace WebApplication3.Controllers
{[Authorize]
    public class productController : Controller
    {
        private ApplicationDbContext db= new ApplicationDbContext();
        // GET: product
        public ActionResult addproduct()
        {
            return View();
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult addproduct(Product Product)
        {
            //Product.ExpireDate = Convert.ToDateTime("06-01-2016");
            if (ModelState.IsValid)
            {
                db.Products.Add(Product);
                db.SaveChanges();
               // ViewData["message"] = "insert sucessfully";
                return RedirectToAction("ProductList");
            }
            return View();
        }

        public ActionResult ProductList(string searchBy,string search,int? page)
        {
            if (searchBy == "ProductName")
            {
                return View(db.Products.Where(x=>x.ProductName==search|| search==null).ToList().ToPagedList(page ?? 1, 5));
            }
            else
            {
                return View(db.Products.Where(x=>x.ProductName.StartsWith(search)|| search==null).ToList().ToPagedList(page ??1, 5));
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
      
        public ActionResult Edit(Product Product)
        {
            db.Entry(Product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }


        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

    }
}