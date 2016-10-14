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
            Cleanup();
            RunExample();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter15.creditreport");
                context.ExecuteStoreCommand("delete from chapter15.creditriskorder");
                context.ExecuteStoreCommand("delete from chapter15.creditriskcustomer");
                context.ExecuteStoreCommand("delete from chapter15.preferredOrder");
                context.ExecuteStoreCommand("delete from chapter15.customerdiscount");
                context.ExecuteStoreCommand("delete from chapter15.preferredcustomer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var pc = new Customer { Name = "Steven James" };
                var rc = new Customer { Name = "Kathy Naudot" };
                pc.PreferredOrders.Add(new Order { Amount = 19.95M });
                pc.CustomerDiscount = new CustomerDiscount { PurchaseDiscount = 10 };
                rc.RiskyOrders.Add(new Order { Amount = 29.99M });
                rc.CreditReports.Add(new CreditReport { CreditRating = 630 });
                context.PreferredCustomers.AddObject(pc);
                context.RiskyCustomers.AddObject(rc);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Preferred Customers");
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var customer in context.PreferredCustomers)
                {
                    Console.WriteLine("Name: {0}, Discount = {1}%", customer.Name, customer.CustomerDiscount.PurchaseDiscount.ToString());
                    foreach (var order in customer.PreferredOrders)
                    {
                        Console.WriteLine("\tOrder: {0}", order.Amount.ToString("C"));
                    }
                }
                Console.WriteLine("\nRisky Customers");
                foreach (var customer in context.RiskyCustomers)
                {
                    Console.WriteLine("Name: {0}", customer.Name);
                    foreach (var order in customer.RiskyOrders)
                    {
                        Console.WriteLine("\tOrder: {0}", order.Amount.ToString("C"));
                    }
                    foreach (var report in customer.CreditReports)
                    {
                        Console.WriteLine("\tCredit Score: {0}", report.CreditRating.ToString());
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
