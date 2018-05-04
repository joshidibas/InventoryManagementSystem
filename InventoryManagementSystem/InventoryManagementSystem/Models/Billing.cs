using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class Billing
    {
        public int SalesID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int PricePerItem { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int BillingAmount { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime DateCreated { get; set; }

        public Billing()
        {

        } 

        public static IEnumerable<Billing> GetBill(IMSEntities db, int? SalesID)
        {
            SqlParameter proParam = new SqlParameter("SalesID", SalesID);

            string objs = @"
                select  p.ProductName, p.PricePerItem, s.BillingDate, s.BillingAmounnt, ps.Quantity, ps.Discount, s.SalesID 
                from product p 
                inner join productsales ps on p.ProductID = s.ProductID 
                inner join sales s on ps.SalesID = s.SalesID where ps.SalesID = @SalesID";
            object[] parameters = new object[] { proParam };
            var result = db.Database.SqlQuery<Billing>(objs, parameters);
            return result;
        }
    }
}