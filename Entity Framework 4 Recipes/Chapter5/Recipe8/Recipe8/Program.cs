using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe8
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
                context.ExecuteStoreCommand("delete from chapter5.contractor");
                context.ExecuteStoreCommand("delete from chapter5.project");
                context.ExecuteStoreCommand("delete from chapter5.manager");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var man1 = new Manager { Name = "Jill Stevens" };
                var proj = new Project { Name = "City Riverfront Park", Manager = man1 };
                var con1 = new Contractor { Name = "Robert Alvert", Project = proj };
                var con2 = new Contractor { Name = "Alan Jones", Project = proj };
                var con3 = new Contractor { Name = "Nancy Roberts", Project = proj };
                context.Projects.AddObject(proj);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var project = context.Projects.Include("Manager").First();
                if (project.ManagerReference.IsLoaded)
                    Console.WriteLine("Manager entity is loaded.");
                else
                    Console.WriteLine("Manager entity is NOT loaded.");
                if (project.Contractors.IsLoaded)
                    Console.WriteLine("Contractors are loaded.");
                else
                    Console.WriteLine("Contractors are NOT loaded.");

                Console.WriteLine("Calling project.Contractors.Load()...");
                project.Contractors.Load();

                if (project.Contractors.IsLoaded)
                    Console.WriteLine("Contractors are now loaded.");
                else
                    Console.WriteLine("Contractors failed to load.");
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
