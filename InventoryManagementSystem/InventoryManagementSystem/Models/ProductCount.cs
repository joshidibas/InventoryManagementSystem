using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagementSystem.Models
{
    public class ProductCount
    {
        public int Total { get; set; }

        public static IEnumerable<ProductCount> Count1(IMSEntities db)
        {
            string objs = @"
                select sum(s.QuantityRemaining) as 'Total' from Product p
                inner join stock s on p.productid = s.ProductID
                inner join ProductType pt on p.ProductTypeID = pt.ProductTypeID 
                where pt.ProductTypeID =  1";
            var result = db.Database.SqlQuery<ProductCount>(objs);
            return result;
        }

        public static IEnumerable<ProductCount> Count2(IMSEntities db)
        {
            string objs = @"
                select sum(s.QuantityRemaining) as 'Total' from Product p
                inner join stock s on p.productid = s.ProductID
                inner join ProductType pt on p.ProductTypeID = pt.ProductTypeID 
                where pt.ProductTypeID =  2";
            var result = db.Database.SqlQuery<ProductCount>(objs);
            return result;
        }

        public static IEnumerable<ProductCount> Count3(IMSEntities db)
        {
            string objs = @"
                select sum(s.QuantityRemaining) as 'Total' from Product p
                inner join stock s on p.productid = s.ProductID
                inner join ProductType pt on p.ProductTypeID = pt.ProductTypeID 
                where pt.ProductTypeID =  3";
            var result = db.Database.SqlQuery<ProductCount>(objs);
            return result;
        }

        public static IEnumerable<ProductCount> Count4(IMSEntities db)
        {
            string objs = @"
                select sum(s.QuantityRemaining) as 'Total' from Product p
                inner join stock s on p.productid = s.ProductID
                inner join ProductType pt on p.ProductTypeID = pt.ProductTypeID 
                where pt.ProductTypeID =  4";
            var result = db.Database.SqlQuery<ProductCount>(objs);
            return result;
        }
    }
}