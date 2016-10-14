using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe2
{
    class Program
    {
        static void Main(string[] args)
        {
            Cleanup();
            RunExample();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter15.Product");
                context.ExecuteStoreCommand("delete from chapter15.supplier");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var sup1 = new Supplier { SupplierId = 1, Name = "CallCom" };
                var sup2 = new Supplier { SupplierId = 2, Name = "Toys, Ltd." };
                context.Suppliers.AddObject(sup1);
                context.Suppliers.AddObject(sup2);
                context.SaveChanges();
                
                // insert some products directly
                context.ExecuteStoreCommand("insert into chapter15.product(productid,name,description,stockcount,discontinued) values (1,'Flowers','Dozen red roses',4,1)");
                context.ExecuteStoreCommand("insert into chapter15.product(productid,name,description,stockcount,discontinued,supplierid) values (2,'Red Fire Truck',null,null,0,1)");
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var p in context.Products)
                {
                    Console.WriteLine("\nName: {0}", p.Name);
                    Console.WriteLine("Stock Count: {0}", p.StockCount.ToString());
                    Console.WriteLine("Discountinued: {0}", p.Discontinued ? "Yes" : "No");
                    Console.WriteLine("Supplier: {0}", p.SupplierName);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
