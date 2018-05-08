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
    public class UserAccountsController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: UserAccounts
        public ActionResult Index()
        {


            var userAccounts = db.UserAccounts.Include(u => u.UserAccounts2).Include(u => u.UserType).Where(u=>u.UserTypeID==3);
            return View(userAccounts.ToList());
        }

        // GET: UserAccounts/Details/5
        public ActionResult Purchases(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var model = Models.PurchaseInfo.GetPurchaseInfo(db, id);
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    throw new Exception("Selected customer hasn't purchased anything in the last 31 days");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

            }
            return View();
        }

        // GET: UserAccounts/Create
        public ActionResult Create()
        {


            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
           
            ViewBag.UserTypeID = new SelectList(db.UserType.Where(c => c.UserTypeID == 3), "UserTypeID", "UserTypeName");
            return View();
        }
  
        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserAccountID,UserTypeID,Email,Password,DateCreated,DateModified,ModifiedBy")] UserAccounts userAccounts)
        {
            if (ModelState.IsValid)
            {
                if (userAccounts.DateCreated == null)
                {
                    userAccounts.DateCreated = DateTime.Now;
                }
                if (userAccounts.DateModified == null)
                {
                    userAccounts.DateModified = DateTime.Now;
                }
                userAccounts.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.UserAccounts.Add(userAccounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userAccounts.ModifiedBy);
            ViewBag.UserTypeID = new SelectList(db.UserType.Where(c => c.UserTypeID == 3), "UserTypeID", "UserTypeName", userAccounts.UserTypeID);
            return View(userAccounts);
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccounts userAccounts = db.UserAccounts.Find(id);
            if (userAccounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userAccounts.ModifiedBy);
            ViewBag.UserTypeID = new SelectList(db.UserType.Where(c => c.UserTypeID == 3), "UserTypeID", "UserTypeName", userAccounts.UserTypeID);
            return View(userAccounts);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserAccountID,UserTypeID,Email,Password,DateCreated,DateModified,ModifiedBy")] UserAccounts userAccounts)
        {
            if (ModelState.IsValid)
            {
                userAccounts.DateModified = DateTime.Now;
                userAccounts.ModifiedBy = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                db.Entry(userAccounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userAccounts.ModifiedBy);
            ViewBag.UserTypeID = new SelectList(db.UserType, "UserTypeID", "UserTypeName", userAccounts.UserTypeID);
            return View(userAccounts);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccounts userAccounts = db.UserAccounts.Find(id);
            if (userAccounts == null)
            {
                return HttpNotFound();
            }
            return View(userAccounts);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccounts userAccounts = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(userAccounts);
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
