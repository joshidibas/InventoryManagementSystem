using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    public class ProductXDetailsController : Controller
    {
        private IMSEntities db = new IMSEntities();

        // GET: ProductXDetails
        public ActionResult Index()
        {
            var productXDetails = db.ProductXDetails.Include(p => p.Product);
            return View(productXDetails.ToList());
        }

        // GET: ProductXDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductXDetails productXDetails = db.ProductXDetails.Find(id);
            if (productXDetails == null)
            {
                return HttpNotFound();
            }
            return View(productXDetails);
        }

        // GET: ProductXDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName");
            return View();
        }

        // POST: ProductXDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductDetailID,ProductID,UnitCostPrice,Location,ThresholdQuantity,Unit,DateCreated,DateModified")] ProductXDetails productXDetails)
        {
            if (ModelState.IsValid)
            {
                if (productXDetails.DateCreated == null)
                {
                    productXDetails.DateCreated = DateTime.Now;
                }
                if (productXDetails.DateModified == null)
                {
                    productXDetails.DateModified = DateTime.Now;
                }
                db.ProductXDetails.Add(productXDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", productXDetails.ProductID);
            return View(productXDetails);
        }

        // GET: ProductXDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductXDetails productXDetails = db.ProductXDetails.Find(id);
            if (productXDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", productXDetails.ProductID);
            return View(productXDetails);
        }

        // POST: ProductXDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductDetailID,ProductID,UnitCostPrice,Location,ThresholdQuantity,Unit,DateCreated,DateModified")] ProductXDetails productXDetails)
        {
            if (ModelState.IsValid)
            {
                productXDetails.DateModified = DateTime.Now;
                db.Entry(productXDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Product, "ProductID", "ProductName", productXDetails.ProductID);
            return View(productXDetails);
        }

        // GET: ProductXDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductXDetails productXDetails = db.ProductXDetails.Find(id);
            if (productXDetails == null)
            {
                return HttpNotFound();
            }
            return View(productXDetails);
        }

        // POST: ProductXDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductXDetails productXDetails = db.ProductXDetails.Find(id);
            db.ProductXDetails.Remove(productXDetails);
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
