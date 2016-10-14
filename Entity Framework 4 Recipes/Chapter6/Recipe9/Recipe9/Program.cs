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
                context.ExecuteStoreCommand("delete from chapter6.[order]");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var ordered = context.Lookups.OfType<OrderStatus>().First(s => s.Value == "Ordered");
                var shipped = context.Lookups.OfType<OrderStatus>().First(s => s.Value == "Shipped");
                var cash = context.Lookups.OfType<TransactionType>().First(s => s.Value == "Cash");
                var fedex = context.Lookups.OfType<ShippingType>().First(s => s.Value == "FedEx");
                var order = new Order { Amount = 99.97M, OrderStatus = shipped, ShippingType = fedex, TransactionType = cash };
                context.Orders.AddObject(order);
                order = new Order { Amount = 29.99M, OrderStatus = ordered, ShippingType = fedex, TransactionType = cash };
                context.Orders.AddObject(order);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("Active Orders");
                Console.WriteLine("=============");
                foreach (var order in context.Orders)
                {
                    Console.WriteLine("\nOrder: {0}", order.OrderId.ToString());
                    Console.WriteLine("Amount: {0}", order.Amount.ToString("C"));
                    Console.WriteLine("Status: {0}", order.OrderStatus.Value);
                    Console.WriteLine("Shipping via: {0}", order.ShippingType.Value);
                    Console.WriteLine("Paid by: {0}", order.TransactionType.Value);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
