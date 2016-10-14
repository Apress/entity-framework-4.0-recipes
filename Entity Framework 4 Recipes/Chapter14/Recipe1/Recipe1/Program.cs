using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;

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
                context.ExecuteStoreCommand("delete from chapter14.product");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Products.AddObject(new Product { Name = "High Country Backpacking Tent", UnitPrice = 199.95M });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // get the product
                var product = context.Products.SingleOrDefault();
                Console.WriteLine("{0} Unit Price: {1}", product.Name, product.UnitPrice.ToString("C"));

                // delete out of band
                context.ExecuteStoreCommand(@"update chapter14.product set unitprice = 229.95 where productId = @p0", product.ProductId);

                // update the product the via the model
                product.UnitPrice = 239.95M;
                Console.WriteLine("Changing {0}'s Unit Price to: {1}", product.Name, product.UnitPrice.ToString("C"));

                try
                {
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Concurrency Exception! {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception! {0}", ex.Message);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
