using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter6.workertask");
                context.ExecuteStoreCommand("delete from chapter6.worker");
                context.ExecuteStoreCommand("delete from chapter6.task");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var worker = new Worker { Name = "Jim" };
                var task = new Task { Title = "Fold Envelopes" };
                var workertask = new WorkerTask { Task = task, Worker = worker };
                context.WorkerTasks.AddObject(workertask);
                task = new Task { Title = "Mail Letters" };
                workertask = new WorkerTask { Task = task, Worker = worker };
                context.WorkerTasks.AddObject(workertask);
                worker = new Worker { Name = "Sara" };
                task = new Task { Title = "Buy Envelopes" };
                workertask = new WorkerTask { Task = task, Worker = worker };
                context.WorkerTasks.AddObject(workertask);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                Console.WriteLine("Workers and Their Tasks");
                Console.WriteLine("=======================");
                foreach (var worker in context.Workers)
                {
                    Console.WriteLine("\n{0}'s tasks:", worker.Name);
                    foreach (var wt in worker.WorkerTasks)
                    {
                        Console.WriteLine("\t{0}", wt.Task.Title);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
