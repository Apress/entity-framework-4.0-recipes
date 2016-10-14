using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter7.lineitem");
                context.ExecuteStoreCommand("delete from chapter7.invoice");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {

                var invoice1 = new Invoice { BilledTo = "Julie Kerns", InvoiceDate = DateTime.Parse("3/19/2010") };
                var invoice2 = new Invoice { BilledTo = "Jim Stevens", InvoiceDate = DateTime.Parse("3/21/2010") };
                context.LineItems.AddObject(new LineItem { Cost = 99.29M, Invoice = invoice1 });
                context.LineItems.AddObject(new LineItem { Cost = 29.95M, Invoice = invoice1 });
                context.LineItems.AddObject(new LineItem { Cost = 109.95M, Invoice = invoice2 });
                context.SaveChanges();

                // display the line items
                Console.WriteLine("Original set of line items...");
                DisplayLineItems();

                // remove a line item from invoice 1's collection
                var item = invoice1.LineItems.ToList().First();
                invoice1.LineItems.Remove(item);
                context.SaveChanges();
                Console.WriteLine("\nAfter removing a line item from an invoice...");
                DisplayLineItems();

                // remove invoice2
                context.DeleteObject(invoice2);
                context.SaveChanges();
                Console.WriteLine("\nAfter removing an invoice...");
                DisplayLineItems();

                // remove a single line item
                context.DeleteObject(invoice1.LineItems.First());
                context.SaveChanges();
                Console.WriteLine("\nAfter removing a line item...");
                DisplayLineItems();
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }

        static void DisplayLineItems()
        {
            bool found = false;
            using (var context = new EFRecipesEntities())
            {
                foreach (var lineitem in context.LineItems)
                {
                    Console.WriteLine("Line item: Cost {0}", lineitem.Cost.ToString("C"));
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("No line items found!");
        }
    }
}
