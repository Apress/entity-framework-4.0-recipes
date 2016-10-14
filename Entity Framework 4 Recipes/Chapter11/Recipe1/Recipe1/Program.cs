using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Objects.DataClasses;
namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter11.product");
                context.ExecuteStoreCommand("delete from chapter11.category");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var c1 = new Category { CategoryName = "Backpacking Tents" };
                new Product { ProductName = "Hooligan", UnitPrice = 89.99M, Category = c1 };
                new Product { ProductName = "Kraz", UnitPrice = 99.99M, Category = c1 };
                new Product { ProductName = "Sundome", UnitPrice = 49.99M, Category = c1 };
                context.Categories.AddObject(c1);
                var c2 = new Category { CategoryName = "Family Tents" };
                new Product { ProductName = "Evanston", UnitPrice = 169.99M,Category = c2 };
                new Product { ProductName = "Montana", UnitPrice = 149.99M, Category = c2 };
                context.Categories.AddObject(c2);
                context.SaveChanges();
            }

            // with esql
            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using esql for the query...");
                Console.WriteLine();
                string sql = @"Select c.CategoryName, EFRecipesModel.AverageUnitPrice(c) as AveragePrice from EFRecipesEntities.Categories as c";
                var cats = context.CreateQuery<DbDataRecord>(sql);
                foreach (var cat in cats)
                {
                    Console.WriteLine("Category '{0}' has an average price of {1}", cat[0], ((decimal)cat[1]).ToString("C"));
                }
            }

            // with LINQ
            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Using LINQ for the query...");
                Console.WriteLine();
                var cats = from c in context.Categories
                           select new { Name = c.CategoryName, AveragePrice = MyFunctions.AverageUnitPrice(c) };
                foreach (var cat in cats)
                {
                    Console.WriteLine("Category '{0}' has an average price of {1}", cat.Name, cat.AveragePrice.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class MyFunctions
    {
        [EdmFunction("EFRecipesModel", "AverageUnitPrice")]
        public static decimal AverageUnitPrice(Category category)
        {
            throw new NotSupportedException("Direct calls are not supported!");
        }
    }
}
