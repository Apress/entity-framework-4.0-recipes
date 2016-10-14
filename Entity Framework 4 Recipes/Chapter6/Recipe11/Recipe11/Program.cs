using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe11
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter6.weborder");
            }

            using (var context = new EFRecipesEntities())
            {
                var order = new WebOrder {CustomerName = "Jim Allen", OrderDate = DateTime.Parse("5/3/2009"),IsDeleted = false, Amount = 200};
                context.WebOrders.AddObject(order);
                order = new WebOrder { CustomerName = "John Stevens", OrderDate = DateTime.Parse("1/1/2006"), IsDeleted = false, Amount = 400 };
                context.WebOrders.AddObject(order);
                order = new WebOrder { CustomerName = "Russel Smith", OrderDate = DateTime.Parse("1/3/2006"), IsDeleted = true, Amount = 500 };
                context.WebOrders.AddObject(order);
                order = new WebOrder { CustomerName = "Mike Hammer", OrderDate = DateTime.Parse("3/6/2006"), IsDeleted = true, Amount = 1800 };
                context.WebOrders.AddObject(order);
                order = new WebOrder { CustomerName = "Steve Jones", OrderDate = DateTime.Parse("1/1/2003"), IsDeleted = true, Amount = 600 };
                context.WebOrders.AddObject(order);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Orders");
                Console.WriteLine("======");
                foreach (var order in context.WebOrders)
                {
                    Console.WriteLine("\nCustomer: {0}", order.CustomerName);
                    Console.WriteLine("OrderDate: {0}", order.OrderDate.ToShortDateString());
                    Console.WriteLine("Is Deleted: {0}", order.IsDeleted.ToString());
                    Console.WriteLine("Amount: {0}", order.Amount.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
