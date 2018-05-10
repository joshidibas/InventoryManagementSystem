using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                HttpContext httpContext = System.Web.HttpContext.Current;
                int UserAccountID = Convert.ToInt32(HttpContext.Session["UserAccountID"]);
                if (UserAccountID != 0)
                {
                    using (IMSEntities db = new IMSEntities())
                    {
                        var RunningOutOfStock = ProductList.GetItemsRunningOutOfStock(db).ToList();
                        ViewBag.RunningOutOfStock = RunningOutOfStock;

                        var ItemsNotSoldLongTime = ProductList.GetItemsNotSoldLongTime(db).ToList();
                        ViewBag.ItemsNotSoldLongTime = ItemsNotSoldLongTime;

                        var ItemsOutOfStock = ProductList.GetItemsOutOfStock(db).ToList();
                        ViewBag.ItemsOutOfStock = ItemsOutOfStock;

                        var InactiveCustomerList = PurchaseInfo.InactiveCustomerList(db).ToList();
                        ViewBag.InactiveCustomerList = InactiveCustomerList;

                        var ItemsNotSold31Days = ProductList.GetItemsNotSold31Days(db).ToList();
                        ViewBag.ItemsNotSold31Days = ItemsNotSold31Days;

  
                        var TotalSuppliers = Statistics.CountSuppliers(db).FirstOrDefault();
                        if (TotalSuppliers != null)
                        {
                            ViewBag.TotalSuppliers = TotalSuppliers.TotalSuppliers;
                        }
                        else
                        {
                            ViewBag.TotalSuppliers = 0;
                        }

                        var TotalCustomers = Statistics.CountCustomers(db).FirstOrDefault();
                        if (TotalCustomers != null)
                        {
                            ViewBag.TotalCustomers = TotalCustomers.TotalCustomers;
                        }
                        else
                        {
                            ViewBag.TotalCustomers = 0;
                        }

                        var TotalProducts = Statistics.CountTotalProducts(db).FirstOrDefault();
                        if (TotalProducts != null)
                        {
                            ViewBag.TotalProducts = TotalProducts.TotalProducts;
                        }
                        else
                        {
                            ViewBag.TotalProducts = 0;
                        }
                        //try { 
                        //var sales = Statistics.Sales(db).FirstOrDefault();

                        //if (sales == null)
                        //{
                        //    ViewBag.Sales = 0;
                        //}
                        //else
                        //{
                        //    ViewBag.Sales = sales.DailySales;
                        //}
                        //}
                        //catch
                        //{
                        //}
                    }
                    return View();
                }
                else
                {
                    throw new Exception("Not Authenticated");
                }
            }
            catch (Exception ex)
            {
                return View("Authentication");
            }

        }

        public ActionResult Authentication()
        {
            HttpContext.Session.RemoveAll();
            ViewBag.Message = "Authentication page.";
            return View();
        }

        [HttpPost]
        public ActionResult Authentication(string email, string password)
        {
            try
            {
                if (email != null && password != null)
                {
                    using (IMSEntities db = new IMSEntities())
                    {
                        var userAccount = db.UserAccounts.Where(u => u.Email == email).Where(u => u.Password == password).FirstOrDefault();
                        if(userAccount != null)
                        {
                            HttpContext.Session.Add("UserAccountID", userAccount.UserAccountID);
                            HttpContext.Session.Add("UserTypeID", userAccount.UserTypeID);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            throw new Exception("Invalid Email or Password");
                        }
                    }
                }
                else
                {
                    throw new Exception("Fields Empty");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }
    }
}