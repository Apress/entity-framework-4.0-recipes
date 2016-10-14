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
                context.ExecuteStoreCommand("delete from chapter5.ceo");
                context.ExecuteStoreCommand("delete from chapter5.projectmanager");
                context.ExecuteStoreCommand("delete from chapter5.supervisor");
                context.ExecuteStoreCommand("delete from chapter5.associate");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var ceo = new CEO { Name = "Joan Miller" };
                var super = new Supervisor { Name = "Bill Mayer", Manager = ceo };
                var pm = new ProjectManager { Name = "Jill Williams", Manager = super };
                context.Associates.AddObject(ceo);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var ceo = context.Associates.First(a => a.ReportsTo == null);
                var associates = context.Associates.ToList();
                PrintDetails(ceo);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }

        static void PrintDetails(Associate associate)
        {
            Console.WriteLine("{0} is a {1}", associate.Name, associate.GetType().Name);
            Console.WriteLine("\t{0} reports to {1}",associate.Name, associate.Manager != null ? associate.Manager.Name : "No One!");
            foreach (var e in associate.TeamMembers)
            {
                PrintDetails(e);
            }
        }
    }
}
