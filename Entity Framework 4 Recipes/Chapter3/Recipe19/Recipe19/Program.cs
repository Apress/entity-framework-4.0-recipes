using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe19
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
                context.ExecuteStoreCommand("delete from chapter3.[order]");
                context.ExecuteStoreCommand("delete from chapter3.account");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var a1 = new Account { City = "Raytown", State = "MO" };
                a1.Orders.Add(new Order { Amount = 223.09M, ShipCity = "Raytown", ShipState = "MO" });
                a1.Orders.Add(new Order { Amount = 189.32M, ShipCity = "Olathe", ShipState = "KS" });

                var a2 = new Account { City = "Kansas City", State = "MO" };
                a2.Orders.Add(new Order { Amount = 99.29M, ShipCity = "Kansas City", ShipState = "MO" });
                
                var a3 = new Account { City = "North Kansas City", State = "MO"};
                a3.Orders.Add(new Order { Amount = 102.29M, ShipCity = "Overland Park", ShipState = "KS" });
                context.Accounts.AddObject(a1);
                context.Accounts.AddObject(a2);
                context.Accounts.AddObject(a3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var orders = from o in context.Orders
                             join a in context.Accounts on 
                                        new {Id = o.AccountId, City = o.ShipCity, State = o.ShipState } 
                                        equals 
                                        new {Id = a.AccountId, City = a.City, State = a.State }
                             select o;

                Console.WriteLine("Orders shipped to the account's city, state...");
                foreach (var order in orders)
                {
                    Console.WriteLine("\tOrder {0} for {1}", order.AccountId.ToString(), order.Amount.ToString("C"));
                } 
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
