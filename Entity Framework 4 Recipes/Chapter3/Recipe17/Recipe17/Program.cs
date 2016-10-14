using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Recipe17
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
                context.ExecuteStoreCommand("delete from chapter3.event");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Events.AddObject(new Event { Name = "TechFest 2010", State = "TX", City = "Dallas" });
                context.Events.AddObject(new Event { Name = "Little Blue River Festival", State = "MO", City = "Raytown" });
                context.Events.AddObject(new Event { Name = "Fourth of July Fireworks", State = "MO", City = "Raytown" });
                context.Events.AddObject(new Event { Name = "BBQ Ribs Championship", State = "TX", City = "Dallas" });
                context.Events.AddObject(new Event { Name = "Thunder on the Ohio", State = "KY", City = "Louisville" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using LINQ");
                var results = from e in context.Events
                             group e by new { e.State, e.City } into g
                             select new
                                 {
                                     State = g.Key.State,
                                     City = g.Key.City,
                                     Events = g
                                 };
                Console.WriteLine("Events by State and City...");
                foreach (var item in results)
                {
                    Console.WriteLine("{0}, {1}", item.City, item.State);
                    foreach (var ev in item.Events)
                    {
                        Console.WriteLine("\t{0}", ev.Name);
                    }
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("\nUsing Entity SQL");
                var esql = @"select e.State, e.City, GroupPartition(e) as Events
                             from Events as e
                             group by e.State, e.City";
                var records = context.CreateQuery<DbDataRecord>(esql);
                Console.WriteLine("Events by State and City...");
                foreach (var rec in records)
                {
                    Console.WriteLine("{0}, {1}", rec["City"], rec["State"]);
                    var events = (List<Event>)rec["Events"];
                    foreach (var ev in events)
                    {
                        Console.WriteLine("\t{0}", ev.Name);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
