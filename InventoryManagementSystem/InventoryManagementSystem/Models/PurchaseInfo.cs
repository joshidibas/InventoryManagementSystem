using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace InventoryManagementSystem.Models
{
    public class PurchaseInfo
    {
        public int UserAccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime LastPurchase { get; set; }

        public PurchaseInfo(int userAccountID, string firstName, string lastName, string productName, int quantity, DateTime billingDate)
        {
            UserAccountID = userAccountID;
            FirstName = firstName;
            LastName = lastName;
            ProductName = productName;
            Quantity = quantity;
            BillingDate = billingDate;
        }

        public PurchaseInfo()
        {

        }

        public static IEnumerable<PurchaseInfo> GetPurchaseInfo(IMSEntities db, int? UserAccountID)
        {
            SqlParameter uaIDParam = new SqlParameter("UserAccountID", UserAccountID);

            string objs = @"
                select ua.UserAccountID, ud.firstname, ud.LastName, p.ProductName, ps.quantity, s.billingdate from UserDetails ud
                inner join UserAccounts ua on ud.UserAccountID=ua.UserAccountID
                inner join sales s on ua.UserAccountID=s.UserAccountID
                inner join ProductSales ps on ps.SalesID=s.SalesID
                inner join Product p on ps.ProductID=p.ProductID
                where  s.BillingDate>= (GETDATE()-31) and ua.useraccountid = @UserAccountID";
            object[] parameters = new object[] { uaIDParam };
            var result = db.Database.SqlQuery<PurchaseInfo>(objs, parameters);
            return result;
        }

        public static IEnumerable<PurchaseInfo> InactiveCustomerList (IMSEntities db)
        {
            string objs = @"
                select distinct ua.UserAccountID, ua.Email, ud.FirstName, ud.LastName, ps.DateCreated as 'LastPurchase'
                from ProductSales ps
                inner join Sales s on ps.SalesID = s.SalesID
                inner join UserAccounts ua on s.UserAccountID = ua.UserAccountID
                inner join UserDetails ud on ua.UserAccountID = ud.UserAccountID 
                where ps.DateCreated <= (GETDATE()-31)";
            var result = db.Database.SqlQuery<PurchaseInfo>(objs);
            return result;
        }

    }
}