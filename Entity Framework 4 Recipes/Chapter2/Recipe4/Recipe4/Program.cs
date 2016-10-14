using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe4
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
                context.ExecuteStoreCommand("delete from chapter2.orderitem");
                context.ExecuteStoreCommand("delete from chapter2.[order]");
                context.ExecuteStoreCommand("delete from chapter2.item");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var order = new Order { OrderId = 1, OrderDate = new DateTime(2010, 1, 18) };
                var item = new Item { SKU = 1729, Description = "Backpack", Price = 29.97M };
                var oi = new OrderItem { Order = order, Item = item, Count = 1 };
                item = new Item { SKU = 2929, Description = "Water Filter", Price = 13.97M };
                oi = new OrderItem { Order = order, Item = item, Count = 3 };
                item = new Item { SKU = 1847, Description = "Camp Stove", Price = 43.99M };
                oi = new OrderItem { Order = order, Item = item, Count = 1 };
                context.Orders.AddObject(order);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var order in context.Orders)
                {
                    Console.WriteLine("Order # {0}, ordered on {1}", order.OrderId.ToString(), order.OrderDate.ToShortDateString());
                    Console.WriteLine("SKU\tDescription\tQty\tPrice");
                    Console.WriteLine("---\t-----------\t---\t-----");
                    foreach (var oi in order.OrderItems)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", oi.Item.SKU, oi.Item.Description, oi.Count.ToString(), oi.Item.Price.ToString("C"));
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
