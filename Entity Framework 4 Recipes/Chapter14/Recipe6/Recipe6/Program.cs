using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Recipe6
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
                context.ExecuteStoreCommand("delete from chapter14.Instructor");
                context.ExecuteStoreCommand("delete from chapter14.Student");
                context.ExecuteStoreCommand("delete from chapter14.Person");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var student = new Student { Name = "Joan Williams", EnrollmentDate = DateTime.Parse("1/12/2010") };
                var instructor = new Instructor { Name = "Rodger Keller", HireDate = DateTime.Parse("7/14/1992") };
                context.People.AddObject(student);
                context.People.AddObject(instructor);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // find the student and update the enrollment date
                var student = context.People.OfType<Student>().First(s => s.Name == "Joan Williams");
                Console.WriteLine("Updating {0}'s enrollment date", student.Name);

                // out-of-band update occurs
                Console.WriteLine("[Apply rogue update]");
                context.ExecuteStoreCommand(@"update chapter14.person set name = 'Joan Smith'
                        where personId = 
                         (select personId from chapter14.person where name = 'Joan Williams')");

                // change the enrollment date
                student.EnrollmentDate = DateTime.Parse("5/2/2010");
                try
                {
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
