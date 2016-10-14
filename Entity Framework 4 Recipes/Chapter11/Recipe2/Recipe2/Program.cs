using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Objects.DataClasses;

namespace Recipe2
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
                context.ExecuteStoreCommand("delete from chapter11.invoice");
                context.ExecuteStoreCommand("delete from chapter11.[order]"); // because we re-used tables in this chapter!
                context.ExecuteStoreCommand("delete from chapter11.customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                DateTime d1 = DateTime.Parse("8/8/2009");
                DateTime d2 = DateTime.Parse("8/12/2008");
                var c1 = new Customer { Name = "Jill Robinson", City = "Dallas" };
                var c2 = new Customer { Name = "Jerry Jones", City = "Denver" };
                var c3 = new Customer { Name = "Janis Brady", City = "Dallas" };
                var c4 = new Customer { Name = "Steve Foster", City = "Dallas" };
                context.Invoices.AddObject(new Invoice { Amount = 302.99M, Description = "New Tires", Date = d1, Customer = c1 });
                context.Invoices.AddObject(new Invoice { Amount = 430.39M, Description = "Brakes and Shocks", Date = d1, Customer = c2 });
                context.Invoices.AddObject(new Invoice { Amount = 102.28M, Description = "Wheel Alignment", Date = d1, Customer = c3 });
                context.Invoices.AddObject(new Invoice { Amount = 629.82M, Description = "A/C Repair", Date = d2, Customer = c4 });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using eSql query...");
                string sql = @"Select value i from 
                         EFRecipesModel.GetInvoices(EFRecipesEntities.Invoices) as i
                         where i.Date > DATETIME'2009-05-1 00:00' 
                         and i.Customer.City = @City";
                var invoices = context.CreateQuery<Invoice>(sql, new ObjectParameter("City", "Dallas")).Include("Customer");
                foreach (var invoice in invoices)
                {
                    Console.WriteLine("Customer: {0}\tInvoice for: {1}, Amount: {2}", invoice.Customer.Name, invoice.Description, invoice.Amount);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Using LINQ query...");
                DateTime date = DateTime.Parse("5/1/2009");
                var invoices = from invoice in 
                                MyFunctions.GetInvoices(context.Invoices)
                               where invoice.Date > date
                               where invoice.Customer.City == "Dallas"
                               select invoice;
                foreach (var invoice in ((ObjectQuery<Invoice>)invoices).Include("Customer"))
                {
                    Console.WriteLine("Customer: {0}, Invoice for: {1}, Amount: {2}", invoice.Customer.Name, invoice.Description, invoice.Amount);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class MyFunctions
    {
        [EdmFunction("EFRecipesModel", "GetInvoices")]
        public static IQueryable<Invoice> GetInvoices(IQueryable<Invoice> invoices)
        {
            return invoices.Provider.CreateQuery<Invoice>(
                Expression.Call((MethodInfo)MethodInfo.GetCurrentMethod(), 
                                Expression.Constant(invoices, typeof(IQueryable<Invoice>))));
        }
    }
}
