using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe11
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
                context.ExecuteStoreCommand("delete from chapter3.accident");
                context.ExecuteStoreCommand("delete from chapter3.worker");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var worker1 = new Worker { Name = "John Kearney" };
                var worker2 = new Worker { Name = "Nancy Roberts" };
                var worker3 = new Worker { Name = "Karla Gibbons" };
                context.Accidents.AddObject(new Accident { Description = "Cuts and contusions", Severity = 3, Worker = worker1 });
                context.Accidents.AddObject(new Accident {Description = "Broken foot",Severity = 4, Worker = worker1});
                context.Accidents.AddObject(new Accident {Description = "Fall, no injuries",Severity = 1, Worker = worker2});
                context.Accidents.AddObject(new Accident {Description = "Minor burn",Severity = 3, Worker = worker2});
                context.Accidents.AddObject(new Accident {Description = "Back strain", Severity = 2, Worker =worker3});
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = false;
                var query = from w in context.Workers
                            select new
                            {
                                Worker = w,
                                Accidents = w.Accidents.Where(a => a.Severity > 2)
                            };
                query.ToList();
                var workers = query.Select(r => r.Worker);
                Console.WriteLine("Workers with serious accidents...");
                foreach (var worker in workers)
                {
                    Console.WriteLine("{0} had the following accidents", worker.Name);
                    if (worker.Accidents.Count == 0)
                        Console.WriteLine("\t--None--");
                    foreach (var accident in worker.Accidents)
                    {
                        Console.WriteLine("\t{0}, severity: {1}", accident.Description, accident.Severity.ToString());
                    }
                }
            }

            Console.WriteLine();

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = false;
                foreach (var worker in context.Workers)
                {
                    Console.WriteLine("{0} had the following accidents", worker.Name);
                    var accidents = worker.Accidents.CreateSourceQuery().Where(a => a.Severity > 2);
                    worker.Accidents.Attach(accidents);
                    if (worker.Accidents.Count == 0)
                        Console.WriteLine("\t--None--");
                    foreach (var accident in accidents)
                    {
                        Console.WriteLine("\t{0}, severity: {1}", accident.Description, accident.Severity.ToString());
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
