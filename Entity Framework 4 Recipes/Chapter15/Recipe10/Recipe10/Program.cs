using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe10
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
                context.ExecuteStoreCommand("delete from chapter15.workorder");
                context.ExecuteStoreCommand("delete from chapter15.priorityworkorder");
            }
        }
        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var wo1 = new WorkOrder { RequestDate = DateTime.Parse("11/04/09"), Problem = "Printer needs paper in shipping.", IsPriority = false };
                var wo2 = new WorkOrder { RequestDate = DateTime.Parse("11/04/09"), Problem = "Main site database server is down!", IsPriority = true };
                var wo3 = new WorkOrder { RequestDate = DateTime.Parse("11/04/09"), Problem = "Backup job complete, remove tape.", IsPriority = false };
                context.WorkOrders.AddObject(wo1);
                context.WorkOrders.AddObject(wo2);
                context.WorkOrders.AddObject(wo3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Work Orders");
                Console.WriteLine("===========");
                foreach (var wo in context.WorkOrders)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", wo.RequestDate.ToShortDateString(), wo.Problem, wo.IsPriority ? "High" : "Normal");
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
