using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.ELinq;
using System.Data.Objects.SqlClient;
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
                context.ExecuteStoreCommand("delete from chapter11.appointment");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var app1 = new Appointment { StartsAt = DateTime.Parse("4/7/2010 14:00"), GoesTo = DateTime.Parse("4/7/2010 15:00") };
                var app2 = new Appointment { StartsAt = DateTime.Parse("4/8/2010 9:00"), GoesTo = DateTime.Parse("4/8/2010 11:00") };
                var app3 = new Appointment { StartsAt = DateTime.Parse("4/8/2010 13:00"), GoesTo = DateTime.Parse("4/7/2010 15:00") };
                context.Appointments.AddObject(app1);
                context.Appointments.AddObject(app2);
                context.Appointments.AddObject(app3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var apps = from a in context.Appointments
                           where SqlFunctions.DatePart("WEEKDAY", a.StartsAt) == 5
                           select a;
                Console.WriteLine("Appointments for Thursday");
                Console.WriteLine("=========================");
                foreach (var appointment in apps)
                {
                    Console.WriteLine("Appointment from {0} to {1}", appointment.StartsAt.ToShortTimeString(), appointment.GoesTo.ToShortTimeString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
