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

                        var TotalBalwa = ProductCount.CountBalwa(db).FirstOrDefault();
                        if (TotalBalwa != null)
                        {
                            ViewBag.TotalBalwa = TotalBalwa.Total;
                        }
                        else
                        {
                            ViewBag.TotalBalwa = 0;
                        }

                        var TotalGitti = ProductCount.CountGitti(db).FirstOrDefault();
                        if (TotalGitti != null)
                        {
                            ViewBag.TotalGitti = TotalGitti.Total;
                        }
                        else
                        {
                            ViewBag.TotalGitti = 0;
                        }

                        var TotalCement = ProductCount.CountCement(db).FirstOrDefault();
                        if (TotalCement != null)
                        {
                            ViewBag.TotalCement = TotalCement.Total;
                        }
                        else
                        {
                            ViewBag.TotalCement = 0;
                        }

                        var TotalRod = ProductCount.CountRod(db).FirstOrDefault();
                        if (TotalRod != null)
                        {
                            ViewBag.TotalRod = TotalRod.Total;
                        }
                        else
                        {
                            ViewBag.TotalRod = 0;
                        }

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

                        var TotalSales = Statistics.CountTotalSales(db).FirstOrDefault();
                        if (TotalSales != null)
                        {
                            ViewBag.TotalSales = TotalSales.TotalSales;
                        }
                        else
                        {
                            ViewBag.TotalSales = 0;
                        }

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