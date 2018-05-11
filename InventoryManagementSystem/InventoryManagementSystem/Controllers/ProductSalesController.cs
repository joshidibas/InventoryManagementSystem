using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModel;

namespace InventoryManagementSystem.Controllers
{
    public class ProductSalesController : Controller
    {
        private IMSEntities db = new IMSEntities();
        // GET: ProductSales
        public ActionResult Index(string sortOrder)
        {
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var productSales = db.ProductSales.Include(p => p.Product).Include(p => p.UserAccounts).Include(p => p.Sales);
            switch (sortOrder)
            {
                
                case "Date":
                    productSales = productSales.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    productSales = productSales.OrderByDescending(s => s.DateCreated);
                    break;
                default:
                    productSales = productSales.OrderByDescending(s => s.DateCreated);
                    break;
            }
           
            return View(productSales.ToList());
        }

        // GET: ProductSales/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSales productSales = db.ProductSales.Find(id);
            if (productSales == null)
            {
                return HttpNotFound();
            }
            return View(productSales);
        }

        // GET: ProductSales/Create
        public ActionResult Create()
        {


            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName");
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.SalesID = new SelectList(db.Sales, "SalesID", "SalesID");
            return View();
        }

        // POST: ProductSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductSalesID,ProductID,SalesID,Quantity,DateCreated,DateModified,ModifiedBy")] ProductSales productSales)
        {
            try
            {  
                if (ModelState.IsValid)
                {
                    var quantityRequest = productSales.Quantity;
                    var result = db.Stock.Where(p => p.ProductID == productSales.ProductID).FirstOrDefault();
                    var quantityRemaning = result.QuantityRemaining;
                    //if remaining qunatity >= requested quantity
                    if (quantityRemaning >= quantityRequest)
                    {
                        if (productSales.DateCreated == null)
                        {
                            productSales.DateCreated = DateTime.Now;
                        }
                        if (productSales.DateModified == null)
                        {
                            productSales.DateModified = DateTime.Now;
                        }
                        productSales.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                        db.ProductSales.Add(productSales);
                        db.SaveChanges();

                        using (IMSEntities db = new IMSEntities())
                        {

                            int productId = productSales.ProductID;
                            var quantity = productSales.Quantity;
                            var product = QueryHelper.GetProductPrice(db, productId).FirstOrDefault();
                            var totalAmount = quantity * product.PricePerItem;
                            var bill = productSales.SalesID;
                            await QueryHelper.UpdateBillingAmount(db, totalAmount, bill);
                            var quantityremaining = QueryHelper.GetQuantity(db, productId).FirstOrDefault();
                            var totalquantityremaining = quantityremaining.QuantityRemaining - quantity;
                            await QueryHelper.UpdateQuantityRemaining(db, totalquantityremaining, productId);
                        }
                    }

                    else
                    {
                        throw new Exception("Insufficient item in the stock");
                    }
                }
                return RedirectToAction("Index");
            }

            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", productSales.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", productSales.ModifiedBy);
            ViewBag.SalesID = new SelectList(db.Sales, "SalesID", "SalesID", productSales.SalesID);

            return View(productSales);

        }

        // GET: ProductSales/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSales productSales = db.ProductSales.FirstOrDefault();
            if (productSales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", productSales.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", productSales.ModifiedBy);
            ViewBag.SalesID = new SelectList(db.Sales, "SalesID", "SalesID", productSales.SalesID);
            return View(productSales);
        }

        // POST: ProductSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductSalesID,ProductID,SalesID,Quantity,DateCreated,DateModified,ModifiedBy")] ProductSales productSales)
        {
            if (ModelState.IsValid)
            {
                productSales.DateModified = DateTime.Now;
                productSales.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.Entry(productSales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", productSales.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", productSales.ModifiedBy);
            ViewBag.SalesID = new SelectList(db.Sales, "SalesID", "SalesID", productSales.SalesID);
            return View(productSales);
        }

        // GET: ProductSales/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSales productSales = db.ProductSales.FirstOrDefault();
            if (productSales == null)
            {
                return HttpNotFound();
            }
            return View(productSales);
        }

        // POST: ProductSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSales productSales = db.ProductSales.FirstOrDefault();
            db.ProductSales.Remove(productSales);
            db.SaveChanges();
            return RedirectToAction("Index");
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
