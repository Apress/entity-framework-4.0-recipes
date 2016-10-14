using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe9
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
                context.ExecuteStoreCommand("delete from chapter5.appointment");
                context.ExecuteStoreCommand("delete from chapter5.doctor");
                context.ExecuteStoreCommand("delete from chapter5.patient");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var doc1 = new Doctor { Name = "Joan Meyers" };
                var doc2 = new Doctor { Name = "Steven Mills" };
                var pat1 = new Patient { Name = "Bill Rivers" };
                var pat2 = new Patient { Name = "Susan Stevenson" };
                var pat3 = new Patient { Name = "Roland Marcy" };
                var app1 = new Appointment { Date = DateTime.Today, Doctor = doc1, Fee = 109.92M, Patient = pat1, Reason = "Checkup" };
                var app2 = new Appointment { Date = DateTime.Today, Doctor = doc2, Fee = 129.87M, Patient = pat2, Reason = "Arm Pain" };
                var app3 = new Appointment { Date = DateTime.Today, Doctor = doc1, Fee = 99.23M, Patient = pat3, Reason = "Back Pain" };
                context.Doctors.AddObject(doc1);
                context.Doctors.AddObject(doc2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var doc = context.Doctors.First(o => o.Name == "Joan Meyers");
                if (!doc.Appointments.IsLoaded)
                {
                    doc.Appointments.Load();
                    Console.WriteLine("Dr. {0}'s appointments were lazy loaded.", doc.Name);
                }
                Console.WriteLine("Dr. {0} has {1} appointment(s).", doc.Name, doc.Appointments.Count().ToString());

                foreach (var app in context.Appointments)
                {
                    if (!app.DoctorReference.IsLoaded)
                    {
                        app.DoctorReference.Load();
                        Console.WriteLine("Dr. {0} was lazy loaded.", app.Doctor.Name);
                    }
                    else
                        Console.WriteLine("Dr. {0} was already loaded.", app.Doctor.Name);
                }

                Console.WriteLine("There are {0} appointments for Dr. {1}", doc.Appointments.Count().ToString(), doc.Name);
                doc.Appointments.Clear();
                Console.WriteLine("Collection clear()'ed");
                Console.WriteLine("There are now {0} appointments for Dr. {1}", doc.Appointments.Count().ToString(), doc.Name);
                doc.Appointments.Load();
                Console.WriteLine("Collection loaded()'ed");
                Console.WriteLine("There are now {0} appointments for Dr. {1}", doc.Appointments.Count().ToString(), doc.Name);
                doc.Appointments.Load(MergeOption.OverwriteChanges);
                Console.WriteLine("Collection loaded()'ed with MergeOption.OverwriteChanges");
                Console.WriteLine("There are now {0} appointments for Dr. {1}", doc.Appointments.Count().ToString(), doc.Name);
            }

            // demonstrating loading part of the collection then Load()'ing the rest
            using (var context = new EFRecipesEntities())
            {
                // load the first doctor and attach just the first appointment
                var doc = context.Doctors.First(o => o.Name == "Joan Meyers");
                doc.Appointments.Attach(doc.Appointments.CreateSourceQuery().Take(1));
                Console.WriteLine("Dr. {0} has {1} appointments loaded.", doc.Name, doc.Appointments.Count().ToString());

                // when we need all of the remaining appointments, simply Load() them
                doc.Appointments.Load();
                Console.WriteLine("Dr. {0} has {1} appointments loaded.", doc.Name, doc.Appointments.Count().ToString());
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
