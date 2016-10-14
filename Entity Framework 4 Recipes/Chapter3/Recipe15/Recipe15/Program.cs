using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe15
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
                context.ExecuteStoreCommand("delete from chapter3.registration");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Registrations.AddObject(new Registration { StudentName = "Jill Rogers", RegistrationDate = DateTime.Parse("12/03/2009 9:30 pm") });
                context.Registrations.AddObject(new Registration { StudentName = "Steven Combs", RegistrationDate = DateTime.Parse("12/03/2009 10:45 am") });
                context.Registrations.AddObject(new Registration { StudentName = "Robin Rosen", RegistrationDate = DateTime.Parse("12/04/2009 11:18 am") });
                context.Registrations.AddObject(new Registration { StudentName = "Allen Smith", RegistrationDate = DateTime.Parse("12/04/2009 3:31 pm") });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var groups = from r in context.Registrations
                             group r by EntityFunctions.TruncateTime(r.RegistrationDate) into g
                             select g;
                foreach (var element in groups)
                {
                    Console.WriteLine("Registrations for {0}", ((DateTime)element.Key).ToShortDateString());
                    foreach (var registration in element)
                    {
                        Console.WriteLine("\t{0}", registration.StudentName);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
