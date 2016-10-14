using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter6.invoice");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Invoices.AddObject(new Invoice { Amount = 19.95M, Description = "Oil Change", Date = DateTime.Parse("4/11/10") });
                context.Invoices.AddObject(new Invoice { Amount = 129.95M, Description = "Wheel Alignment", Date = DateTime.Parse("4/01/10") });
                context.Invoices.AddObject(new DeletedInvoice { Amount = 39.95M, Description = "Engine Diagnosis", Date = DateTime.Parse("4/01/10") });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var invoice in context.Invoices)
                {
                    var isDeleted = invoice as DeletedInvoice;
                    Console.WriteLine("{0} Invoice", isDeleted == null ? "Active" : "Deleted");
                    Console.WriteLine("Description: {0}", invoice.Description);
                    Console.WriteLine("Amount: {0}", invoice.Amount.ToString("C"));
                    Console.WriteLine("Date: {0}", invoice.Date.ToShortDateString());
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
