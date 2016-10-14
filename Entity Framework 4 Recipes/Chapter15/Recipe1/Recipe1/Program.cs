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
                context.ExecuteStoreCommand("delete from chapter15.contact");
                context.ExecuteStoreCommand("delete from chapter15.account");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var acc1 = new Account { Balance = 99.34M };
                var con1 = new Contact { Name = "Stacy Jones", Phone = "867-5301" };
                var cus1 = new Customer { Name = "Bill Waters", Phone = "907-2212", Account = acc1 };
                context.Contacts.AddObject(con1);
                context.Contacts.AddObject(cus1);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("All Contacts");
                Console.WriteLine("============");
                foreach (var contact in context.Contacts)
                {
                    Console.WriteLine("{0} {1}", contact.Name, contact.Phone);
                }

                Console.WriteLine("Just Customers");
                foreach (var contact in context.Contacts.OfType<Customer>())
                {
                    Console.WriteLine("\t{0} {1} (Balance: {2})", contact.Name, contact.Phone, contact.Account.Balance.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
