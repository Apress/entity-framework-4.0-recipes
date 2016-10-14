using System;
using System.Linq;
using System.Data;

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
                context.ExecuteStoreCommand("delete from chapter12.PurchaseOrder");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.PurchaseOrders.AddObject(new PurchaseOrder { Amount = 109.98M});
                context.PurchaseOrders.AddObject(new PurchaseOrder { Amount = 20.99M });
                context.PurchaseOrders.AddObject(new PurchaseOrder { Amount = 208.89M});
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Purchase Orders");
                foreach (var po in context.PurchaseOrders)
                {
                    Console.WriteLine("Purchase Order: {0}", po.PurchaseOrderId.ToString(""));
                    Console.WriteLine("\tPaid: {0}", po.Paid ? "Yes" : "No");
                    Console.WriteLine("\tAmount: {0}", po.Amount.ToString("C"));
                    Console.WriteLine("\tCreated On: {0}", po.CreateDate.ToShortTimeString());
                    Console.WriteLine("\tModified at: {0}", po.ModifiedDate.ToShortTimeString());
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
            this.SavingChanges += new EventHandler(EFRecipesEntities_SavingChanges);
        }

        void EFRecipesEntities_SavingChanges(object sender, EventArgs e)
        {
            var pos = this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified).Select(entry => entry.Entity).OfType<PurchaseOrder>().ToList();
            foreach (var order in pos)
            {
                if (order.EntityState == EntityState.Added)
                {
                    order.PurchaseOrderId = Guid.NewGuid();
                    order.CreateDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                }
                else if (order.EntityState == EntityState.Modified)
                {
                    order.ModifiedDate = DateTime.Now;
                }
            }
        }
    }
}
