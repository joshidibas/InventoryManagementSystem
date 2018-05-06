using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.ViewModel
{
    public class QueryHelper
    {
        public static Task<int> UpdateBillingAmount(IMSEntities db, int? BillingAmount, int SalesID)
        {

            SqlParameter amountParam = new SqlParameter("BillingAmount", BillingAmount);
            SqlParameter salesParam = new SqlParameter("SalesID", SalesID);
            if(BillingAmount == null) { 
            try
            {
                string objs = @"
                UPDATE Sales SET BillingAmount = @BillingAmount
                WHERE SalesID = @SalesID";
                object[] parameters = new object[] { amountParam, salesParam };
                var result = db.Database.ExecuteSqlCommandAsync(objs, parameters);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            }
            else {
                try
                {
                    string objs = @"
                UPDATE Sales SET BillingAmount = BillingAmount + @BillingAmount
                WHERE SalesID = @SalesID";
                    object[] parameters = new object[] { amountParam, salesParam };
                    var result = db.Database.ExecuteSqlCommandAsync(objs, parameters);
                    return result;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    
            
        

        public static IEnumerable<Product> GetProductPrice (IMSEntities db, int ProductID)
        {
            SqlParameter proParam = new SqlParameter("ProductID", ProductID);

            string objs = @"
                SELECT * FROM Product
                WHERE ProductID = @ProductID";
            object[] parameters = new object[] { proParam };
            var result = db.Database.SqlQuery<Product>(objs, parameters);
            return result;
        }

        public static IEnumerable<Stock> GetQuantity(IMSEntities db, int ProductID)
        {
            SqlParameter proParam = new SqlParameter("ProductID", ProductID);

            string objs = @"
                SELECT * FROM Stock
                WHERE ProductID = @ProductID";
            object[] parameters = new object[] { proParam };
            var result = db.Database.SqlQuery<Stock>(objs, parameters);
            return result;
        }

        public static Task<int> UpdateQuantityRemaining(IMSEntities db, int? QuantityRemaining, int ProductID)
        {

            SqlParameter quantParam = new SqlParameter("QuantityRemaining", QuantityRemaining);
            SqlParameter proParam = new SqlParameter("ProductID", ProductID);
            try
            {
                string objs = @"
                UPDATE Stock SET QuantityRemaining = @QuantityRemaining
                WHERE ProductID = @ProductID";
                object[] parameters = new object[] { quantParam, proParam };
                var result = db.Database.ExecuteSqlCommandAsync(objs, parameters);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<int> UpdatePassowrd (IMSEntities db, int? UserAccountID, string Password)
        {

            SqlParameter IDParam = new SqlParameter("UserAccountID", UserAccountID);
            SqlParameter PasswordParam = new SqlParameter("Password", Password);
            try
            {
                string objs = @"
                UPDATE UserAccounts SET Password = @Password
                WHERE UserAccountID = @UserAccountID";
                object[] parameters = new object[] { IDParam, PasswordParam };
                var result = db.Database.ExecuteSqlCommandAsync(objs, parameters);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}