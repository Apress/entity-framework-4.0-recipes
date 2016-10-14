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
                context.ExecuteStoreCommand("delete from Chapter10.Customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var c1 = new Customer {Name = "Robin Steele", Company = "GoShopNow.com", ContactTitle="CEO"};
                var c2 = new Customer {Name = "Orin Torrey", Company = "GoShopNow.com", ContactTitle="Sales Manager"};
                var c3 = new Customer {Name = "Robert Lancaster", Company = "GoShopNow.com", ContactTitle = "Sales Manager"};
                var c4 = new Customer { Name = "Julie Stevens", Company = "GoShopNow.com", ContactTitle = "Sales Manager" };
                context.Customers.AddObject(c1);
                context.Customers.AddObject(c2);
                context.Customers.AddObject(c3);
                context.Customers.AddObject(c4);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var allCustomers = context.GetCustomers("GoShopNow.com", "Sales Manager");
                Console.WriteLine("Customers that are Sales Managers at GoShopNow.com");
                foreach (var c in allCustomers)
                {
                    Console.WriteLine("Customer: {0}", c.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
