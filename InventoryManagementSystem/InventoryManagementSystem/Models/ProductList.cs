using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class ProductList
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }  
        public int QuantityRemaining { get; set; }
        public DateTime LastSold { get; set; }

        public ProductList(int productID, string productName, string productTypeName, string description, int quantityRemaining)
        {
            ProductID = productID;
            ProductName = productName;           
            QuantityRemaining = quantityRemaining;
        }

        public ProductList()
        {

        }

        public static IEnumerable<ProductList> GetItemsStocked(IMSEntities db, int? ProductTypeID)
        {
            SqlParameter proTypeParam = new SqlParameter("ProductTypeID", ProductTypeID);

            string objs = @"
                select s.ProductId, p.ProductName, s.QuantityRemaining from ProductType pt
                inner join Product p on pt.ProductTypeID = p.ProductTypeID
                inner join stock s on p.ProductID = s.ProductID
                where pt.ProductTypeID = @ProductTypeID";
            object[] parameters = new object[] { proTypeParam };
            var result = db.Database.SqlQuery<ProductList>(objs, parameters);
            return result;
        }

        public static IEnumerable<ProductList> GetItemsRunningOutOfStock(IMSEntities db)
        {
            string objs = @"
                select p.ProductName, s.QuantityRemaining from stock s
                inner join Product p on s.productid= p.ProductID where s.QuantityRemaining<= 10";
            var result = db.Database.SqlQuery<ProductList>(objs);
            return result;
        }

        public static IEnumerable<ProductList> GetItemsNotSoldLongTime (IMSEntities db)
        {
            string objs = @"
                select distinct p.ProductID, p.ProductName, ps.DateCreated as 'LastSold' 
                from ProductSales ps 
                inner join product p on ps.ProductID = p.ProductID
                inner join ProductType pt on p.ProductTypeID = pt.ProductTypeID
                inner join stock s on ps.ProductID = s.ProductID 
                where ps.DateCreated <= (GETDATE() - 100) 
                AND s.QuantityRemaining != 0";
            var result = db.Database.SqlQuery<ProductList>(objs);
            return result;
        }

        public static IEnumerable<ProductList> GetItemsOutOfStock(IMSEntities db)
        {
            string objs = @"
                select p.ProductName, s.QuantityRemaining from stock s
                inner join Product p on s.productid=p.ProductID where s.QuantityRemaining = 0";
            var result = db.Database.SqlQuery<ProductList>(objs);
            return result;
        }

        public static IEnumerable<ProductList> GetItemsNotSold31Days(IMSEntities db)
        {
            string objs = @"
                select distinct p.ProductID, p.ProductName, p.Description, pt.ProductTypeName, ps.DateCreated as 'LastSold' 
                from ProductSales ps 
                inner join product p on ps.ProductID = p.ProductID
                inner join ProductType pt on p.ProductTypeID = pt.ProductTypeID
                inner join stock s on ps.ProductID = s.ProductID 
                where ps.DateCreated <= (GETDATE() - 31) 
                AND s.QuantityRemaining != 0";
            var result = db.Database.SqlQuery<ProductList>(objs);
            return result;
        }

    }
}