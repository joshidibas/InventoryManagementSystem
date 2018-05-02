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
    public class StockManagmentsController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: StockManagments
        public ActionResult Index()
        {


            var stockManagment = db.StockManagment.Include(s => s.Stock).Include(s => s.UserAccounts).Include(s => s.UserAccounts1);
            return View(stockManagment.ToList());
        }

        // GET: StockManagments/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockManagment stockManagment = db.StockManagment.Find(id);
            if (stockManagment == null)
            {
                return HttpNotFound();
            }
            return View(stockManagment);
        }

        // GET: StockManagments/Create
        public ActionResult Create()
        {


            ViewBag.StockID = new SelectList(db.Stock, "StockID", "StockID");
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            return View();
        }

        // POST: StockManagments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockManagementID,StockID,OperationType,OperationDate,Quantity,UserAccountID,DateCreated,DateModified,ModifiedBy")] StockManagment stockManagment)
        {
            if (ModelState.IsValid)
            {
                db.StockManagment.Add(stockManagment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockID = new SelectList(db.Stock, "StockID", "StockID", stockManagment.StockID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", stockManagment.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", stockManagment.UserAccountID);
            return View(stockManagment);
        }

        // GET: StockManagments/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockManagment stockManagment = db.StockManagment.Find(id);
            if (stockManagment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockID = new SelectList(db.Stock, "StockID", "StockID", stockManagment.StockID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", stockManagment.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", stockManagment.UserAccountID);
            return View(stockManagment);
        }

        // POST: StockManagments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockManagementID,StockID,OperationType,OperationDate,Quantity,UserAccountID,DateCreated,DateModified,ModifiedBy")] StockManagment stockManagment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockManagment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockID = new SelectList(db.Stock, "StockID", "StockID", stockManagment.StockID);
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", stockManagment.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", stockManagment.UserAccountID);
            return View(stockManagment);
        }

        // GET: StockManagments/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockManagment stockManagment = db.StockManagment.Find(id);
            if (stockManagment == null)
            {
                return HttpNotFound();
            }
            return View(stockManagment);
        }

        // POST: StockManagments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockManagment stockManagment = db.StockManagment.Find(id);
            db.StockManagment.Remove(stockManagment);
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
