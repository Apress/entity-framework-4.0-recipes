using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter10.person");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.People.AddObject(new Instructor { Name = "Karen Stanford", Salary = 62500M });
                context.People.AddObject(new Instructor { Name = "Robert Morris", Salary = 61800M });
                context.People.AddObject(new Student { Name = "Jill Mathers", Degree = "Computer Science" });
                context.People.AddObject(new Student { Name = "Steven Kennedy", Degree = "Math" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Instructors and Students");
                var allPeople = context.GetAllPeople();
                foreach (var person in allPeople)
                {
                    if (person is Instructor)
                        Console.WriteLine("Instructor {0} makes {1}/year", person.Name, ((Instructor)person).Salary.ToString("C"));
                    else if (person is Student)
                        Console.WriteLine("Student {0}'s major is {1}", person.Name, ((Student)person).Degree);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
