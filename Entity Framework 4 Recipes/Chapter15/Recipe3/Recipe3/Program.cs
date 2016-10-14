using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter15.ProjectEmployee");
                context.ExecuteStoreCommand("delete from chapter15.project");
                context.ExecuteStoreCommand("delete from chapter15.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var proj = new Project { Name = "Highway 101 Access Route" };
                proj.Members.Add(new Employee { Name = "Jim Stone" });
                proj.Members.Add(new Employee { Name = "Roland Jones" });
                proj.Members.Add(new Employee { Name = "Jennifer Collins" });
                proj.ProjectManager = new Employee { Name = "Sue Raven" };
                context.Projects.AddObject(proj);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var p in context.Projects)
                {
                    Console.WriteLine("Project: {0}, Manager: {1}", p.Name, p.ProjectManager.Name);
                    Console.WriteLine("Members:");
                    foreach (var m in p.Members)
                    {
                        Console.WriteLine("\t{0}", m.Name);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
