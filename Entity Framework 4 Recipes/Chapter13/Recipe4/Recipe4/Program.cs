using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace Recipe4
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Cleanup();
                InsertData();
            }
            else
            {
                GetTimes();
            }
            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }

        static void Cleanup()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter13.courtdate");
                context.ExecuteStoreCommand("delete from chapter13.client");
                context.ExecuteStoreCommand("delete from chapter13.accountant");
                context.ExecuteStoreCommand("delete from chapter13.lawyer");
                context.ExecuteStoreCommand("delete from chapter13.person");
                context.ExecuteStoreCommand("delete from chapter13.college");
            }
        }

        static void InsertData()
        {
            using (var context = new EFRecipesEntities())
            {
                var c1 = new College { Name = "Mid-Missouri State" };
                var c2 = new College { Name = "Florida Western University" };
                var p1 = new Lawyer { Name = "Bill Jordan", College = c1 };
                p1.CourtDates.Add(new CourtDate { Appointment = DateTime.Parse("3/02/10 10:00 am"), Name = "John Dillinger" });
                var p2 = new Accountant { Name = "Sue Redmon", College = c2 };
                p2.Clients.Add(new Client { Name = "Robin Rosen" });
                context.People.AddObject(p1);
                context.People.AddObject(p2);
                context.SaveChanges();
                Console.WriteLine("Data inserted...");
            }
        }

        static void GetTimes()
        {
            using (var context = new EFRecipesEntities())
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var lawyer = context.People.Include("College").OfType<Lawyer>().Include("CourtDates").First();
                stopwatch.Stop();
                Console.WriteLine("Execution Time: {0}", stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
