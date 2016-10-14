using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe13
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
                context.ExecuteStoreCommand("delete from chapter6.toyota");
                context.ExecuteStoreCommand("delete from chapter6.bmw");
                context.ExecuteStoreCommand("delete from chapter6.cardealer");
                context.ExecuteStoreCommand("delete from chapter6.dealer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var d1 = new Dealer { Name = "All Cities Toyota" };
                var d2 = new Dealer { Name = "Southtown Toyota" };
                var d3 = new Dealer { Name = "Luxury Auto World" };
                var c1 = new Toyota { Model = "Camry", Color = "Green", Year = "2010", Dealer = d1 };
                var c2 = new BMW { Model = "310i", Color = "Blue", CollisionAvoidance = true, Year = "2010", Dealer = d3 };
                var c3 = new Toyota { Model = "Tundra", Color = "Blue", Year = "2010", Dealer = d2 };
                context.Dealers.AddObject(d1);
                context.Dealers.AddObject(d2);
                context.Dealers.AddObject(d3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("Dealers and Their Cars");
                Console.WriteLine("======================");
                foreach (var dealer in context.Dealers)
                {
                    Console.WriteLine("\nDealer: {0}", dealer.Name);
                    foreach(var car in dealer.Cars)
                    {
                        string make = string.Empty;
                        if (car is Toyota)
                            make = "Toyota";
                        else if (car is BMW)
                            make = "BMW";
                        Console.WriteLine("\t{0} {1} {2} {3}", car.Year, car.Color, make, car.Model);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
