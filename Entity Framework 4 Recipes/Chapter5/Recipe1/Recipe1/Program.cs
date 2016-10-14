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
                context.ExecuteStoreCommand("delete from chapter5.customeremail");
                context.ExecuteStoreCommand("delete from chapter5.customer");
                context.ExecuteStoreCommand("delete from chapter5.customertype");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var web = new CustomerType { Description = "Web Customer", CustomerTypeId = 1 };
                var retail = new CustomerType { Description = "Retail Customer", CustomerTypeId = 2 };
                var customer = new Customer { Name = "Joan Smith", CustomerType = web };
                customer.CustomerEmails.Add(new CustomerEmail { Email = "jsmith@gmail.com" });
                customer.CustomerEmails.Add(new CustomerEmail { Email = "joan@smith.com" });
                context.Customers.AddObject(customer);
                customer = new Customer { Name = "Bill Meyers", CustomerType = retail };
                customer.CustomerEmails.Add(new CustomerEmail { Email = "bmeyers@gmail.com" });
                context.Customers.AddObject(customer);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var customers = context.Customers.Include("CustomerType").Include("CustomerEmails");
                Console.WriteLine("Customers");
                Console.WriteLine("=========");
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} is a {1}, email address(es)", customer.Name, customer.CustomerType.Description);
                    foreach (var email in customer.CustomerEmails)
                    {
                        Console.WriteLine("\t{0}", email.Email);
                    }
                }
            }
            
            using (var context = new EFRecipesEntities())
            {
                var customTypes = context.CustomerTypes.Include("Customers.CustomerEmails");
                Console.WriteLine("\nCustomers by Type");
                Console.WriteLine("=================");
                foreach (var customerType in customTypes)
                {
                    Console.WriteLine("Customer type: {0}", customerType.Description);
                    foreach (var customer in customerType.Customers)
                    {
                        Console.WriteLine("{0}", customer.Name);
                        foreach (var email in customer.CustomerEmails)
                        {
                            Console.WriteLine("\t{0}", email.Email);
                        }
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
