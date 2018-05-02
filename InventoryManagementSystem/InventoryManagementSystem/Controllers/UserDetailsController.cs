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
    public class UserDetailsController : Controller
    {
        private IMSEntities db = new IMSEntities();



        // GET: UserDetails
        public ActionResult Index()
        {


            var userDetails = db.UserDetails.Include(u => u.UserAccounts).Include(u => u.UserAccounts1);
                //.Join(db.UserAccounts, ud => ud.UserAccountID, ua => ua.UserAccountID, (ud, ua) => new {UserAccounts = ua, UserDetails=ud}).Where(u => u.UserAccounts.UserTypeID == 3);
            return View(userDetails.ToList());
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {


            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserDetailD,UserAccountID,FirstName,LastName,BirthDate,Phone,Address,DateCreated,DateModified,ModifiedBy")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userDetails.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", userDetails.UserAccountID);
            return View(userDetails);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userDetails.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", userDetails.UserAccountID);
            return View(userDetails);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserDetailD,UserAccountID,FirstName,LastName,BirthDate,Phone,Address,DateCreated,DateModified,ModifiedBy")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userDetails.ModifiedBy);
            ViewBag.UserAccountID = new SelectList(db.UserAccounts, "UserAccountID", "Email", userDetails.UserAccountID);
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetails userDetails = db.UserDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDetails userDetails = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetails);
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
