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
    public class UsersController : Controller
    {
        private IMSEntities db = new IMSEntities();


        // GET: Users
        public ActionResult Index()
        {


            var userAccounts = db.UserAccounts.Include(u => u.UserAccounts2).Include(u => u.UserType);
            return View(userAccounts.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
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

        // GET: Users/Create
        public ActionResult Create()
        {


            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email");
            ViewBag.UserTypeID = new SelectList(db.UserType, "UserTypeID", "UserTypeName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserAccountID,UserTypeID,Email,Password,DateCreated,DateModified,ModifiedBy")] UserAccounts userAccounts)
        {
            if (ModelState.IsValid)
            {
                db.UserAccounts.Add(userAccounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userAccounts.ModifiedBy);
            ViewBag.UserTypeID = new SelectList(db.UserType, "UserTypeID", "UserTypeName", userAccounts.UserTypeID);
            return View(userAccounts);
        }

        // GET: Users/Edit/5
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
            ViewBag.UserTypeID = new SelectList(db.UserType, "UserTypeID", "UserTypeName", userAccounts.UserTypeID);
            return View(userAccounts);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserAccountID,UserTypeID,Email,Password,DateCreated,DateModified,ModifiedBy")] UserAccounts userAccounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModifiedBy = new SelectList(db.UserAccounts, "UserAccountID", "Email", userAccounts.ModifiedBy);
            ViewBag.UserTypeID = new SelectList(db.UserType, "UserTypeID", "UserTypeName", userAccounts.UserTypeID);
            return View(userAccounts);
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
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
