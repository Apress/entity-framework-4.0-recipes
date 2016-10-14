using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

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
                context.ExecuteStoreCommand("delete from chapter13.appointment");
                context.ExecuteStoreCommand("delete from chapter13.doctor");
                context.ExecuteStoreCommand("delete from chapter13.company");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var company = new Company { Name = "Paola Heart Center" };
                var doc1 = new Doctor { Name = "Jill Mathers", Company = company };
                var doc2 = new Doctor {Name = "Robert Stevens", Company = company};
                var app1 = new Appointment { AppointmentDate = DateTime.Parse("3/18/2010"), Patient = "Karen Rodgers", Doctor = doc1 };
                var app2 = new Appointment { AppointmentDate = DateTime.Parse("3/20/2010"), Patient = "Steven Cook", Doctor = doc2 };
                context.Companies.AddObject(company);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Appointments for Doctors...");
                context.Doctors.MergeOption = MergeOption.NoTracking;
                var doctors = context.Doctors.Include("Company");
                foreach (var doctor in doctors)
                {
                    Console.WriteLine("Doctor: {0} [{1}]", doctor.Name, doctor.Company.Name);
                    Console.WriteLine("Appointments: {0}", doctor.Appointments.Count().ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
