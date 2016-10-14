using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;
using System.Data.Common;

namespace Recipe8
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
                context.ExecuteStoreCommand("delete from chapter11.[order]");
                // we're re-using customer, so we need to delete from invoice as well!
                context.ExecuteStoreCommand("delete from chapter11.invoice");
                context.ExecuteStoreCommand("delete from chapter11.customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var c1 = new Customer { Name = "Jill Masters", City = "Raytown" };
                var c2 = new Customer { Name = "Bob Meyers", City = "Austin" };
                var c3 = new Customer { Name = "Robin Rosen", City = "Dallas" };
                var o1 = new Order { OrderAmount = 12.99M, Customer = c1 };
                var o2 = new Order { OrderAmount = 99.39M, Customer = c2 };
                var o3 = new Order { OrderAmount = 101.29M, Customer = c3 };
                context.Orders.AddObject(o1);
                context.Orders.AddObject(o2);
                context.Orders.AddObject(o3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Customers with above average total purchases");
                var esql = @"select o.Customer.Name, count(o.OrderId) as TotalOrders,
                             Sum(o.OrderAmount) as TotalPurchases
                             from EFRecipesEntities.Orders as o
                             where o.OrderAmount > 
                               anyelement(select value Avg(o.OrderAmount) from
                                          EFRecipesEntities.Orders as o)
                             group by o.Customer.Name";
                var summary = context.CreateQuery<DbDataRecord>(esql);
                foreach (var item in summary)
                {
                    Console.WriteLine("\t{0}, Total Orders: {1}, Total: {2:C}",
                        item["Name"], item["TotalOrders"], item["TotalPurchases"]);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
