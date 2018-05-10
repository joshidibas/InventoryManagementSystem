using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Statistics
    {
        public int TotalSuppliers { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public int TotalSales { get; set; }
        public int DailySales { get; set; }


        public static IEnumerable<Statistics> CountSuppliers(IMSEntities db)
        {
            string objs = @"
                select count(supplierid) as 'TotalSuppliers' from Suppliers";
            var result = db.Database.SqlQuery<Statistics>(objs);
            return result;
        }

        public static IEnumerable<Statistics> CountCustomers(IMSEntities db)
        {
            string objs = @"
                select count(UserAccountID) as 'TotalCustomers' from UserAccounts where usertypeid = 3";
            var result = db.Database.SqlQuery<Statistics>(objs);
            return result;
        }

        public static IEnumerable<Statistics> CountTotalProducts(IMSEntities db)
        {
            string objs = @"
                select sum(QuantityRemaining) as 'TotalProducts' from Stock";
            var result = db.Database.SqlQuery<Statistics>(objs);
            return result;
        }
        public static IEnumerable<Statistics> Sales(IMSEntities db) {
            string objs = @"
            select sum(BillingAmount) as 'TotalSales' from Sales s
                where s.DateCreated > (CAST(CAST(GETDATE() AS DATE) AS DATETIME))" ;
            var result = db.Database.SqlQuery<Statistics>(objs);
            return result;



        }
       
    }
}