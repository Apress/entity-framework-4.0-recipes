using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe9
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
                context.ExecuteStoreCommand("delete from chapter15.[order]");
                context.ExecuteStoreCommand("delete from chapter15.customer");
                context.ExecuteStoreCommand("delete from chapter15.orderstatustype");
                context.ExecuteStoreCommand("delete from chapter15.shippingtype");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                // insert our lookup values
                context.ExecuteStoreCommand("insert into chapter15.orderstatustype(OrderStatusTypeId, Description) values (1,'Processing')");
                context.ExecuteStoreCommand("insert into chapter15.orderstatustype(OrderStatusTypeId, Description) values (2,'Shipped')");
                context.ExecuteStoreCommand("insert into chapter15.shippingtype(ShippingTypeId, Description) values (1,'UPS')");
                context.ExecuteStoreCommand("insert into chapter15.shippingtype(ShippingTypeId, Description) values (2,'FedEx')");
            }

            using (var context = new EFRecipesEntities())
            {
                var c1 = new Customer { FirstName = "Robert", LastName = "Jones" };
                var o1 = new Order { OrderDate = DateTime.Parse("11/19/2009"), OrderStatusTypeId = 2, ShippingTypeId = 1, Customer = c1 };
                var o2 = new Order { OrderDate = DateTime.Parse("12/13/09"), OrderStatusTypeId = 1, ShippingTypeId = 1, Customer = c1 };
                var c2 = new Customer { FirstName = "Julia", LastName = "Stevens" };
                var o3 = new Order { OrderDate = DateTime.Parse("10/19/09"), OrderStatusTypeId = 2, ShippingTypeId = 2, Customer = c2 };
                context.Customers.AddObject(c1);
                context.Customers.AddObject(c2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var c in context.Customers)
                {
                    Console.WriteLine("{0} has {1} order(s)", c.FullName, c.TotalOrders.ToString());
                    foreach (var o in c.Orders)
                    {
                        Console.WriteLine("\tOrdered on: {0}", o.OrderDate.ToShortDateString());
                        Console.WriteLine("\tStatus: {0}", o.OrderStatus);
                        Console.WriteLine("\tShip via: {0}\n", o.ShippingType);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
