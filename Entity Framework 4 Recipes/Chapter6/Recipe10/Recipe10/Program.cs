using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe10
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
                context.ExecuteStoreCommand("delete from chapter6.refurbishedtoy");
                context.ExecuteStoreCommand("delete from chapter6.toy");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand(@"insert into chapter6.toy 
                            (Name,ForDonationOnly) values ('RagDoll',1)");
                var toy = new Toy { Name = "Fuzzy Bear", Price = 9.97M };
                var refurb = new RefurbishedToy { Name = "Derby Car", Price = 19.99M, Quality = "Ok to sell" };
                context.Toys.AddObject(toy);
                context.Toys.AddObject(refurb);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("All Toys");
                Console.WriteLine("========");
                foreach (var toy in context.Toys)
                {
                    Console.WriteLine("{0}", toy.Name);
                }
                Console.WriteLine("\nRefurbished Toys");
                foreach (var toy in context.Toys.OfType<RefurbishedToy>())
                {
                    Console.WriteLine("{0}, Price = {1}, Quality = {2}", toy.Name, toy.Price, ((RefurbishedToy)toy).Quality);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
