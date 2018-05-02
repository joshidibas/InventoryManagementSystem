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
    public class SupplierDetailsController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: SupplierDetails
        public ActionResult Index()
        {


            var supplierDetail = db.SupplierDetail.Include(s => s.UserAccounts).Include(s => s.Suppliers);
            return View(supplierDetail.ToList());
        }

        // GET: SupplierDetails/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierDetail supplierDetail = db.SupplierDetail.Find(id);
            if (supplierDetail == null)
            {
                return HttpNotFound();
            }
            return View(supplierDetail);
        }

        // GET: SupplierDetails/Create
        public ActionResult Create()
        {


            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail");
            return View();
        }

        // POST: SupplierDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierDetailID,SupplierID,ContactNumber,Address,ContactPersonName,DateCreated,DateModified,ModifiedBy")] SupplierDetail supplierDetail)
        {
            if (ModelState.IsValid)
            {
                db.SupplierDetail.Add(supplierDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", supplierDetail.ModifiedBy);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail", supplierDetail.SupplierID);
            return View(supplierDetail);
        }

        // GET: SupplierDetails/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierDetail supplierDetail = db.SupplierDetail.Find(id);
            if (supplierDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", supplierDetail.ModifiedBy);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail", supplierDetail.SupplierID);
            return View(supplierDetail);
        }

        // POST: SupplierDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierDetailID,SupplierID,ContactNumber,Address,ContactPersonName,DateCreated,DateModified,ModifiedBy")] SupplierDetail supplierDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", supplierDetail.ModifiedBy);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierEmail", supplierDetail.SupplierID);
            return View(supplierDetail);
        }

        // GET: SupplierDetails/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierDetail supplierDetail = db.SupplierDetail.Find(id);
            if (supplierDetail == null)
            {
                return HttpNotFound();
            }
            return View(supplierDetail);
        }

        // POST: SupplierDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierDetail supplierDetail = db.SupplierDetail.Find(id);
            db.SupplierDetail.Remove(supplierDetail);
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
