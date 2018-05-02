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
    public class ProductSuppliersController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: ProductSuppliers
        public ActionResult Index()
        {


            var ProductSupplier = db.ProductSupplier.Include(p => p.Product).Include(p => p.UserAccounts).Include(p => p.Suppliers);
            return View(ProductSupplier.ToList());
        }

        // GET: ProductSuppliers/Details/5
        public ActionResult Details(int? ptid,int? sid)
        {


            if (sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier ProductSupplier = db.ProductSupplier.Find(ptid, sid);
            if (ProductSupplier == null)
            {
                return HttpNotFound();
            }
            return View(ProductSupplier);
        }

        // GET: ProductSuppliers/Create
        public ActionResult Create()
        {


            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductTypeID", "ProductTypeName");
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail");
            return View();
        }

        // POST: ProductSuppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductSupplierID,ProductTypeID,SupplierID,DateCreated,DateModified,ModifiedBy")] ProductSupplier ProductSupplier)
        {
            if (ModelState.IsValid)
            {
                db.ProductSupplier.Add(ProductSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductID", "ProductTypeName", ProductSupplier.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", ProductSupplier.ModifiedBy);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail", ProductSupplier.SupplierID);
            return View(ProductSupplier);
        }

        // GET: ProductSuppliers/Edit/5
        public ActionResult Edit(int? ptid, int?sid)
        {


            if (sid == null && ptid==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier ProductSupplier = db.ProductSupplier.Find(ptid,sid);
            if (ProductSupplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductID", "ProductTypeName", ProductSupplier.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", ProductSupplier.ModifiedBy);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail", ProductSupplier.SupplierID);
            return View(ProductSupplier);
        }

        // POST: ProductSuppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductSupplierID,ProductTypeID,SupplierID,DateCreated,DateModified,ModifiedBy")] ProductSupplier ProductSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ProductSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ProductID", "ProductTypeName", ProductSupplier.ProductID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", ProductSupplier.ModifiedBy);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail", ProductSupplier.SupplierID);
            return View(ProductSupplier);
        }

        // GET: ProductSuppliers/Delete/5
        public ActionResult Delete(int? ptid, int? sid)
        {


            if (sid == null && ptid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier ProductSupplier = db.ProductSupplier.Find(ptid,sid);
            if (ProductSupplier == null)
            {
                return HttpNotFound();
            }
            return View(ProductSupplier);
        }

        // POST: ProductSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSupplier ProductSupplier = db.ProductSupplier.Find(id);
            db.ProductSupplier.Remove(ProductSupplier);
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
