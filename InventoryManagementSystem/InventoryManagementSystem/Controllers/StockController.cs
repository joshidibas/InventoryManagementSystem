using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Controllers
{
    public class StockController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: Stock
        public ActionResult Index(string searchString)
        {


            var stock = db.Stock.Include(s => s.Product).Include(s => s.UserAccounts);
            if (!String.IsNullOrEmpty(searchString))
            {
                stock = stock.Where(s => s.Product.ProductName.Contains(searchString));

            }
            return View(stock.ToList());
        }

        // GET: Stock/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stock/Create
        public ActionResult Create()
        {


            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName");
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,ProductID,QuantityRemaining,DateCreated,DateModified,ModifiedBy")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                if (stock.DateCreated == null)
                {
                    stock.DateCreated = DateTime.Now;
                }
                if (stock.DateModified == null)
                {
                    stock.DateModified = DateTime.Now;
                }
                stock.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.Stock.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", stock.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", stock.ModifiedBy);
            return View(stock);
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", stock.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", stock.ModifiedBy);
            return View(stock);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockID,ProductID,QuantityRemaining,DateCreated,DateModified,ModifiedBy")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.DateModified = DateTime.Now;
                stock.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", stock.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", stock.ModifiedBy);
            return View(stock);
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stock.Find(id);
            db.Stock.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OutOfStock()
        {


            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
