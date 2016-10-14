using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;

namespace Recipe5
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new EFRecipesEntities())
            {
                // cleanup from previous tests
                context.ExecuteStoreCommand("delete from chapter4.productdetail");
                context.ExecuteStoreCommand("delete from chapter4.orderdetail");
                context.ExecuteStoreCommand("delete from chapter4.product");
                context.ExecuteStoreCommand("delete from chapter4.category");
                context.ExecuteStoreCommand("delete from chapter4.supplier");

                // add in our test data
                var s1 = new Supplier { CompanyName = "Backcountry Supply", Country = "USA" };
                var s2 = new Supplier { CompanyName = "Alpine Tent", Country = "Italy" };
                var s3 = new Supplier { CompanyName = "Ace Footware", Country = "USA" };
                var c1 = new Category { CategoryName = "Tents" };
                var c2 = new Category { CategoryName = "Shoes/Boots" };
                var pd1 = new ProductDetail { UnitPrice = 99.95M };
                var pd2 = new ProductDetail { UnitPrice = 129.95M };
                var pd3 = new ProductDetail { UnitPrice = 39.95M };
                var p1 = new Product { ProductName = "Pup Tent", ProductDescription = "Small and packable tent", Discontinued = true, UnitsInStock = 4 };
                var p2 = new Product { ProductName = "Trail Boot", ProductDescription = "Perfect boot for hiking", Discontinued = false, UnitsInStock = 19 };
                var p3 = new Product { ProductName = "Family Tent", ProductDescription = "Sleeps 2 adults + 2 children", Discontinued = false, UnitsInStock = 10 };
                var od1 = new OrderDetail { UnitPrice = 39.95M, Quantity = 1};
                var od2 = new OrderDetail { UnitPrice = 129.95M, Quantity = 2 };
                var od3 = new OrderDetail { UnitPrice = 99.95M, Quantity = 1 };
                p1.Supplier = s2;
                p1.Category = c1;
                p1.ProductDetail = pd3;
                p1.OrderDetails.Add(od1);
                p2.Supplier = s3;
                p2.Category = c2;
                p2.OrderDetails.Add(od2);
                p2.ProductDetail = pd2;
                p3.Supplier = s1;
                p3.Category = c1;
                p3.ProductDetail = pd1;
                p3.OrderDetails.Add(od3);
                context.Products.AddObject(p1);
                context.Products.AddObject(p2);
                context.Products.AddObject(p3);
                context.SaveChanges();
            }
        }

        protected void ProductsWithCategory(object sender, CustomExpressionEventArgs e)
        {
            if (e.Values["CategoryName"] != null)
            {
                var catnames = e.Values["CategoryName"].ToString().Split(',');
                e.Query = from p in e.Query.Cast<Product>()
                          where catnames.Contains(p.Category.CategoryName)
                          select p;
            }
        }

        static public IQueryable<Product> ProductWithSalesGreaterThan(IQueryable<Product> query, decimal totalSales)
        {
            return from p in query
                   where p.OrderDetails.Sum(od => od.UnitPrice * od.Quantity) > totalSales
                   select p;
        }
    }

    public partial class Product
    {
        public decimal TotalSales
        {
            get
            {
                return this.OrderDetails.Sum(od => od.UnitPrice * od.Quantity);
            }
        }
    }
}