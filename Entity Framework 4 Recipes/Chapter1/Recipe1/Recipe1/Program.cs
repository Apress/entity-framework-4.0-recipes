using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter1.[order]");
                context.ExecuteStoreCommand("delete from chapter1.customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var customer = new Customer {Name = "Phil Marlowe", Email="phil.marlowe@hotmail.com", Phone="876-5309"};
                var order = new Order { DateOrdered = DateTime.Parse("4/18/2010"), InvoiceNumber = 12224, Customer = customer };
                context.Customers.AddObject(customer);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var customer in context.Customers)
                {
                    Console.WriteLine("Customer: {0}, email: {1}", customer.Name, customer.Email);
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine("\t{0} {1}", order.OrderId, order.DateOrdered);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
