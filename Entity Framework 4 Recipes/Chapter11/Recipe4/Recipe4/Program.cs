using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
namespace Recipe4
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
                context.ExecuteStoreCommand("delete from chapter11.associate");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var john = new Supervisor { Name = "John Smith" };
                var steve = new Supervisor {Name = "Steve Johnson"};
                var jill = new ProjectManager { Name = "Jill Masterson", Manager = john };
                var karen = new ProjectManager { Name = "Karen Carns", Manager = steve };
                var bob = new TeamLead { Name = "Bob Richardson", Manager = karen };
                var tom = new TeamLead { Name = "Tom Landers", Manager = jill };
                var nancy = new TeamMember { Name = "Nancy Jones", Manager = tom };
                var stacy = new TeamMember { Name = "Stacy Rutgers", Manager = bob };
                context.Associates.AddObject(john);
                context.Associates.AddObject(steve);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using eSql...");
                var emps = context.Associates.OfType<TeamMember>()
                    .Where(@"EFRecipesModel.GetProjectManager(it).Name = 
                           @projectManager || 
                           EFRecipesModel.GetSupervisor(it).Name == @supervisor",
                    new ObjectParameter("projectManager", "Jill Masterson"),
                    new ObjectParameter("supervisor", "Steve Johnson"));
                Console.WriteLine("Team members that report up to either");
                Console.WriteLine("Project Manager Jill Masterson ");
                Console.WriteLine("or Supervisor Steve Johnson");
                foreach (var emp in emps)
                {
                    Console.WriteLine("\tAssociate: {0}", emp.Name);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Using LINQ...");
                var emps = from e in context.Associates.OfType<TeamMember>()
                           where MyFunctions.GetProjectManager(e).Name == 
                            "Jill Masterson" ||
                           MyFunctions.GetSupervisor(e).Name == "Steve Johnson"
                           select e;
                Console.WriteLine("Team members that report up to either");
                Console.WriteLine("Project Manager Jill Masterson ");
                Console.WriteLine("or Supervisor Steve Johnson");
                foreach (var emp in emps)
                {
                    Console.WriteLine("\tAssociate: {0}", emp.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class MyFunctions
    {
        [EdmFunction("EFRecipesModel", "GetProjectManager")]
        public static ProjectManager GetProjectManager(TeamMember member)
        {
            throw new NotSupportedException("Direct calls not supported.");
        }

        [EdmFunction("EFRecipesModel", "GetSupervisor")]
        public static Supervisor GetSupervisor(TeamMember member)
        {
            throw new NotSupportedException("Direct calls not supported.");
        }
    }
}
