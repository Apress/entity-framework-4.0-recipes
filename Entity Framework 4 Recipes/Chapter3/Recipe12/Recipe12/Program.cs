using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe12
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
                context.ExecuteStoreCommand("delete from chapter3.topselling");
                context.ExecuteStoreCommand("delete from chapter3.product");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var p1 = new Product { Name = "Trailrunner Backpack" };
                var p2 = new Product { Name = "Green River Tent", TopSelling = new TopSelling { Rating = 3 } };
                var p3 = new Product { Name = "Prairie Home Dutch Oven", TopSelling = new TopSelling { Rating = 4 } };
                var p4 = new Product { Name = "QuickFire Fire Starter", TopSelling = new TopSelling { Rating = 2 } };
                context.Products.AddObject(p1);
                context.Products.AddObject(p2);
                context.Products.AddObject(p3);
                context.Products.AddObject(p4);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var products = from p in context.Products
                               orderby p.TopSelling.Rating descending
                               select p;
                Console.WriteLine("Top selling products sorted by rating");
                foreach (var product in products)
                {
                    if (product.TopSelling != null)
                        Console.WriteLine("\t{0} [rating: {1}]", product.Name, product.TopSelling.Rating.ToString());
                }
            }

            using (var context = new EFRecipesEntities())
            {
                var products = from p in context.Products
                               join t in context.TopSellings on p.ProductId equals t.ProductId into g
                               from tps in g.DefaultIfEmpty()
                               orderby tps.Rating descending
                               select new
                                   {
                                       Name = p.Name,
                                       Rating = tps.Rating == null ? 0 : tps.Rating
                                   };

                Console.WriteLine("\nTop selling products sorted by rating");
                foreach (var product in products)
                {
                    if (product.Rating != 0)
                        Console.WriteLine("\t{0} [rating: {1}]", product.Name, product.Rating.ToString());
                }
            }

            using (var context = new EFRecipesEntities())
            {
                var esql = @"select value p from products as p
                             order by case when p.TopSelling is null then 0
                                                else p.TopSelling.Rating end desc";
                var products = context.CreateQuery<Product>(esql);
                Console.WriteLine("\nTop selling products sorted by rating");
                foreach (var product in products)
                {
                    if (product.TopSelling != null)
                        Console.WriteLine("\t{0} [rating: {1}]", product.Name, product.TopSelling.Rating.ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
