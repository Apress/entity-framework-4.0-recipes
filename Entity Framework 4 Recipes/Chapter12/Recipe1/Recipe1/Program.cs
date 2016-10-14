using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Objects;
using System.Data;
namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter12.applicant");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var path1 = "AlexJones.txt";
                File.AppendAllText(path1, "Alex Jones\nResume\n...");
                var path2 = "JanisRogers.txt";
                File.AppendAllText(path2, "Janis Rodgers\nResume\n...");
                var app1 = new Applicant { Name = "Alex Jones", ResumePath = path1 };
                var app2 = new Applicant { Name = "Janis Rogers", ResumePath = path2 };
                context.Applicants.AddObject(app1);
                context.Applicants.AddObject(app2);
                context.SaveChanges();

                // delete Alex Jones
                context.Applicants.DeleteObject(app1);
                context.SaveChanges();
            }

            Console.WriteLine("Press <enter> to continue....");
            Console.ReadLine();
        }
    }

    public partial class EFRecipesEntities
    {
        public override int SaveChanges(SaveOptions options)
        {
            Console.WriteLine("Saving Changes...");
            var applicants = this.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted).Select(e => e.Entity).OfType<Applicant>().ToList();
            int changes = base.SaveChanges(options);
            Console.WriteLine("\n{0} applicants deleted", applicants.Count().ToString());
            foreach (var app in applicants)
            {
                File.Delete(app.ResumePath);
                Console.WriteLine("\n{0}'s resume at {1} deleted", app.Name, app.ResumePath);
            }
            return changes;
        }
    }
}
