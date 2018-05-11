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
    public class SalesController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.UserAccounts).Include(s => s.UserAccounts1);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var GetBill = Billing.GetBill(db, id).ToList();
                ViewBag.GetBill = GetBill;
                var model = Models.Billing.GetBill(db, id).FirstOrDefault();
                return View(model);
                
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

            }
            return View();
        }

        // GET: Sales/Create
        public ActionResult Create()
        {


            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesID,BillingDate,BillingAmount,UserAccountID,DateCreated,DateModified,ModifiedBy")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                if (sales.DateCreated == null)
                {
                    sales.DateCreated = DateTime.Now;
                }
                if (sales.DateModified == null)
                {
                    sales.DateModified = DateTime.Now;
                }
                sales.BillingAmount = 0;
                sales.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.Sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", sales.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", sales.UserAccountID);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", sales.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", sales.UserAccountID);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesID,BillingDate,BillingAmount,UserAccountID,DateCreated,DateModified,ModifiedBy")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                sales.DateModified = DateTime.Now;
                sales.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", sales.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", sales.UserAccountID);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
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
