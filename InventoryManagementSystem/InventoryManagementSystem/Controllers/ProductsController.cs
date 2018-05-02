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
    public class ProductsController : Controller
    {
        private IMSEntities db = new IMSEntities();



        // GET: Products
        public ActionResult Index()
        {


            var product = db.Product.Include(p => p.UserAccounts).Include(p => p.ProductType);
            return View(product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var model = Models.ProductDetails.GetProductDetails(db, id).FirstOrDefault();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    throw new Exception("Selected product is not added to the stock");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;

            }
            return View();

        }

        // GET: Products/Create
        public ActionResult Create()
        {


            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductTypeID,ProductName,PricePerItem,Description,ThresholdQuantity,DateCreated,DateModified,ModifiedBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", product.ModifiedBy);
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductTypeID", "ProductTypeName", product.ProductTypeID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", product.ModifiedBy);
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductTypeID", "ProductTypeName", product.ProductTypeID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductTypeID,ProductName,PricePerItem,Description,ThresholdQuantity,DateCreated,DateModified,ModifiedBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", product.ModifiedBy);
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductTypeID", "ProductTypeName", product.ProductTypeID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
