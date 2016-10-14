using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data;
using System.Data.Common;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter3.customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var cus1 = new Customer { Name = "Robert Stevens", Email = "rstevens@mymail.com" };
                var cus2 = new Customer { Name = "Julia Kerns", Email = "julia.kerns@abc.com" };
                var cus3 = new Customer { Name = "Nancy Whitrock", Email = "nrock@myworld.com" };
                context.Customers.AddObject(cus1);
                context.Customers.AddObject(cus2);
                context.Customers.AddObject(cus3);
                context.SaveChanges();
            }

            // using object services
            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Customers...");
                string esql = "select value c from Customers as c";
                var customers = context.CreateQuery<Customer>(esql);
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}'s email is: {1}", customer.Name, customer.Email);
                }
            }

            Console.WriteLine();

            // using EntityClient
            using (var conn = new EntityConnection("name=EFRecipesEntities"))
            {
                Console.WriteLine("Customers...");
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "select value c from EFRecipesEntities.Customers as c";
                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}'s email is: {1}", reader.GetString(1), reader.GetString(2));
                    }
                }
            }

            Console.WriteLine();

            // using object services without the VALUE keyword
            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Customers...");
                string esql = "select c.Name, c.Email from Customers as c";
                var records = context.CreateQuery<DbDataRecord>(esql);
                foreach (var record in records)
                {
                    var name = record[0] as string;
                    var email = record[1] as string;
                    Console.WriteLine("{0}'s email is: {1}", name, email);
                }
            }

            Console.WriteLine();

            // using EntityClient without the VALUE keyword
            using (var conn = new EntityConnection("name=EFRecipesEntities"))
            {
                Console.WriteLine("Customers...");
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = "select c.Name, C.Email from EFRecipesEntities.Customers as c";
                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}'s email is: {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
