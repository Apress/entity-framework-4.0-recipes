using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter5.orderitem");
                context.ExecuteStoreCommand("delete from chapter5.[order]");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var order = new Order { CustomerName = "Jenny Craig", OrderDate = DateTime.Parse("3/12/2010") };
                var item1 = new OrderItem { Order = order, Shipped = 3, SKU = 2827, UnitPrice = 12.95M };
                var item2 = new OrderItem { Order = order, Shipped = 1, SKU = 1918, UnitPrice = 19.95M };
                var item3 = new OrderItem { Order = order, Shipped = 3, SKU = 392, UnitPrice = 8.95M };
                context.Orders.AddObject(order);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // assume we have an instance of Order
                var order = context.Orders.First();

                // get the total order amount
                var amt = order.OrderItems.CreateSourceQuery().Sum(o => (o.Shipped * o.UnitPrice));
                Console.WriteLine("Order Number: {0}", order.OrderId.ToString());
                Console.WriteLine("Order Date: {0}", order.OrderDate.ToShortDateString());
                Console.WriteLine("Order Total: {0}", amt.ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
