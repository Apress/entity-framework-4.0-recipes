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
                context.ExecuteStoreCommand("delete from chapter3.patient");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Patients.AddObject(new Patient { Name = "Jill Stevens", City = "Dallas" });
                context.Patients.AddObject(new Patient { Name = "Bill Azle", City = "Fort Worth" });
                context.Patients.AddObject(new Patient { Name = "Karen Stanford", City = "Raytown" });
                context.Patients.AddObject(new Patient { Name = "David Frazier", City = "Dallas" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using LINQ Builder Methods");
                var patients = context.Patients.Where(p => p.City == "Dallas");
                foreach (var patient in patients)
                {
                    Console.WriteLine("{0} is in {1}", patient.Name, patient.City);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("\nUsing Entity SQL");
                var patients = context.CreateQuery<Patient>(@"select value p from Patients as p where p.City = 'Dallas'");
                foreach (var patient in patients)
                {
                    Console.WriteLine("{0} is in {1}", patient.Name, patient.City);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("\nUsing ESQL Builder Methods");
                var patients = context.CreateObjectSet<Patient>("Patients").Where("it.City = 'Dallas'");
                foreach (var patient in patients)
                {
                    Console.WriteLine("{0} is in {1}", patient.Name, patient.City);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
