using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe14
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
                context.ExecuteStoreCommand("delete from chapter3.Customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Customers.AddObject(new Customer { Name = "Roberts, Jill", Email = "jroberts@abc.com" });
                context.Customers.AddObject(new Customer { Name = "Robertson, Alice", Email = "arob@gmail.com" });
                context.Customers.AddObject(new Customer { Name = "Rogers, Steven", Email = "srogers@termite.com" });
                context.Customers.AddObject(new Customer { Name = "Roe, Allen", Email = "allenr@umc.com" });
                context.Customers.AddObject(new Customer { Name = "Jones, Chris", Email = "cjones@ibp.com" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                string match = "Ro";
                int pageIndex = 0;
                int pageSize = 3;

                var customers = context.Customers.Where(c => c.Name.StartsWith(match))
                                    .OrderBy(c => c.Name)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize);
                Console.WriteLine("Customers Ro*");
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} [email: {1}]", customer.Name, customer.Email);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                string match = "Ro%";
                int pageIndex = 0;
                int pageSize = 3;

                var customers = context.Customers.Where("it.Name like @Name", new ObjectParameter("Name", match))
                                        .Skip("it.Name", "@Skip", new ObjectParameter("Skip", pageIndex))
                                        .Top("@Limit", new ObjectParameter("Limit", pageSize));
                Console.WriteLine("\nCustomers Ro*");
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} [email: {1}]", customer.Name, customer.Email);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                string match = "Ro%";
                int pageIndex = 0;
                int pageSize = 3;

                var esql = @"select value c from Customers as c 
                             where c.Name like @Name
                             order by c.Name
                             skip @Skip limit @Limit";
                Console.WriteLine("\nCustomers Ro*");
                var customers = context.CreateQuery<Customer>(esql, new[]
                                  {
                                    new ObjectParameter("Name",match),
                                    new ObjectParameter("Skip",pageIndex * pageSize),
                                    new ObjectParameter("Limit",pageSize)
                                  });
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0} [email: {1}]", customer.Name, customer.Email);
                }                              
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
