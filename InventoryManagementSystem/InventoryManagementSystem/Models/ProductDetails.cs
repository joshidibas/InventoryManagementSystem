using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class ProductDetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public int PricePerItem { get; set; }
        public string Description { get; set; }
        //public int ThresholdQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public int QuantityRemaining { get; set; }

        public ProductDetails(int productID, string productName, string productTypeName, string description, int quantityRemaining)
        {
            ProductID = productID;
            ProductName = productName;
            ProductTypeName = productTypeName;
            Description = description;
            QuantityRemaining = quantityRemaining;
        }

        public ProductDetails()
        {

        }

        public static IEnumerable<ProductDetails> GetProductDetails(IMSEntities db, int? ProductID)
        {
            SqlParameter proParam = new SqlParameter("ProductID", ProductID);

            string objs = @"
                select p.ProductID, p.ProductName, pt.ProductTypeName, p.PricePerItem, p.Description, p.DateCreated, s.QuantityRemaining
                from product p 
                inner join stock s on p.ProductID = s.ProductID 
                inner join producttype pt on p.ProductTypeID = pt.ProductTypeID where p.ProductID = @ProductID";
            object[] parameters = new object[] { proParam };
            var result = db.Database.SqlQuery<ProductDetails>(objs, parameters);
            return result;
        }

    }
}