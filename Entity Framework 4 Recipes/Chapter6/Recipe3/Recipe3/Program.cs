using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe3
{
    class Program
    {
        static void Main(string[] args)
        {
            Cleanup();
            RunExample();
            RunExample2();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter6.relatedproduct");
                context.ExecuteStoreCommand("delete from chapter6.product");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var product1 = new Product { Name = "Pole", Price = 12.97M };
                var product2 = new Product { Name = "Tent", Price = 199.95M };
                var product3 = new Product { Name = "Ground Cover", Price = 29.95M };
                product2.RelatedProducts.Add(product3);
                product1.RelatedProducts.Add(product2);
                context.Products.AddObject(product1);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var product2 = context.Products.Include("RelatedProducts").Include("OtherRelatedProducts").First(p => p.Name == "Tent");
                Console.WriteLine("Product: {0} ... {1}", product2.Name, product2.Price.ToString("C"));
                Console.WriteLine("Related Products");
                foreach (var prod in product2.RelatedProducts)
                {
                    Console.WriteLine("\t{0} ... {1}", prod.Name, prod.Price.ToString("C"));
                }
                foreach (var prod in product2.OtherRelatedProducts)
                {
                    Console.WriteLine("\t{0} ... {1}", prod.Name, prod.Price.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }

        static void RunExample2()
        {
            using (var context = new EFRecipesEntities())
            {
                var product1 = new Product { Name = "Pole", Price = 12.97M };
                var product2 = new Product { Name = "Tent", Price = 199.95M };
                var product3 = new Product { Name = "Ground Cover", Price = 29.95M };
                product2.RelatedProducts.Add(product3);
                product1.RelatedProducts.Add(product2);
                context.Products.AddObject(product1);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var product1 = context.Products.First(p => p.Name == "Pole");
                Dictionary<int, Product> t = new Dictionary<int, Product>();
                GetRelated(product1, t);
                Console.WriteLine("Products related to {0}", product1.Name);
                foreach (var key in t.Keys)
                {
                    Console.WriteLine("\t{0}", t[key].Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }

        static void GetRelated(Product p, Dictionary<int, Product> t)
        {
            p.RelatedProducts.Load();
            foreach (var relatedProduct in p.RelatedProducts)
            {
                if (!t.ContainsKey(relatedProduct.ProductId))
                {
                    t.Add(relatedProduct.ProductId, relatedProduct);
                    GetRelated(relatedProduct, t);
                }
            }
            p.OtherRelatedProducts.Load();
            foreach (var otherRelated in p.OtherRelatedProducts)
            {
                if (!t.ContainsKey(otherRelated.ProductId))
                {
                    t.Add(otherRelated.ProductId, otherRelated);
                    GetRelated(otherRelated, t);
                }
            }
        }
    }
}
