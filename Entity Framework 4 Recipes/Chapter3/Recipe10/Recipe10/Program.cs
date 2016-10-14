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
                context.ExecuteStoreCommand("delete from chapter3.organization");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var o1 = new Organization { Name = "ABC Electric", City = "Azle", State = "TX" };
                var o2 = new Organization { Name = "PowWow Pests", City = "Miami", State = "FL" };
                var o3 = new Organization { Name = "Grover Grass & Seed", City = "Fort Worth", State = "TX" };
                context.Organizations.AddObject(o1);
                context.Organizations.AddObject(o2);
                context.Organizations.AddObject(o3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var query = context.Organizations.Where("it.State = 'TX'");
                Console.WriteLine("Cities (using LINQ)");
                var cities = query.Select(o => o.City).Distinct().OrderBy(c => c);
                foreach (var city in cities)
                {
                    Console.WriteLine("{0}", city);
                }

                Console.WriteLine("Cities (using eSQL)");
                cities = query.SelectValue<string>("distinct it.City").OrderBy("it");
                foreach (var city in cities)
                {
                    Console.WriteLine("{0}", city);
                }

                Console.WriteLine("Press <enter> to continue...");
                Console.ReadLine();
            }
        }
    }
}
