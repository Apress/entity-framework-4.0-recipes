using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe5
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
                context.ExecuteStoreCommand("delete from chapter15.invoice");
                context.ExecuteStoreCommand("delete from chapter15.client");
            }
        }

        // Note: This is the non-MEST code version. If you use MEST, change
        // context.Audits.AddObject() to context.Clients.AddObject() and
        // context.Invoices.AddObject() because these entities are now in their
        // own entity sets.
        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var c1 = new Client { Name = "Joanne Wise" };
                var c2 = new Client { Name = "Robert Marr" };
                var c3 = new Client { Name = "Shelly King" };
                var i1 = new Invoice { Amount = 99.23M };
                var i2 = new Invoice { Amount = 29.95M };
                c1.Invoices.Add(i1);
                c3.Invoices.Add(i2);
                context.Audits.AddObject(c1);
                context.Audits.AddObject(c2);

                context.Audits.AddObject(c3);
                context.SaveChanges();
                Console.WriteLine("Waiting 10 seconds to update...");
                System.Threading.Thread.Sleep(10 * 1000);
                i1.Amount = 98.49M;
                i2.Amount = 39.99M;
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("Invoices...");
                foreach (var bill in context.Audits.OfType<Invoice>())
                {
                    Console.WriteLine("{0} Amount: {1}", bill.Client.Name, bill.Amount.ToString("C"));
                    Console.WriteLine("\tCreated: {0}", bill.CreateDate.ToLongTimeString());
                    Console.WriteLine("\tLast Modified: {0}\n", bill.ModifiedDate.ToLongTimeString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.SavingChanges += (o, s) =>
            {
                var inaudits = this.ObjectStateManager
                                   .GetObjectStateEntries(System.Data.EntityState.Added)
                                   .Where(entry => entry.Entity is Audit)
                                   .Select(entry => entry.Entity as Audit);
                foreach (var audit in inaudits)
                {
                    audit.CreateDate = DateTime.Now;
                    audit.ModifiedDate = DateTime.Now;
                }
                var modaudits = this.ObjectStateManager
                            .GetObjectStateEntries(System.Data.EntityState.Modified)
                            .Where(entry => entry.Entity is Audit)
                            .Select(entry => entry.Entity as Audit);
                foreach (var audit in modaudits)
                {
                    audit.ModifiedDate = DateTime.Now;
                }
            };
        }
    }
}
