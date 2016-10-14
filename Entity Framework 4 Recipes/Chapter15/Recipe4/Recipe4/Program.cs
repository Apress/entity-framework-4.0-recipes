using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter15.person");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var p = new Principal { Name = "Jill Robins", HireDate = DateTime.Parse("8/12/2002"), Salary = 72500M };
                var i1 = new Instructor { Name = "Roland Jones", HireDate = DateTime.Parse("8/14/2005"), Salary = 61000M};
                var i2 = new Instructor { Name = "Steven Curtis", HireDate = DateTime.Parse("8/23/1992"), Salary = 68200M };
                context.People.AddObject(new Student { Name = "Karen Roberts" });
                context.People.AddObject(new Student {Name = "Bobby McGivens"});
                context.People.AddObject(new Student {Name = "Janis Hettler"});
                context.People.AddObject(p);
                context.People.AddObject(i1);
                context.People.AddObject(i2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Staff");
                Console.WriteLine("=====");
                foreach (var staff in context.People.OfType<Staff>())
                {
                    Console.WriteLine("\t{0}, Hire date: {1}, Salary: {2} {3})", staff.Name, staff.HireDate.Value.ToShortDateString(),
                        staff.Salary.Value.ToString("C"), staff is Principal ? "Principal" : "Instructor");
                }
                Console.WriteLine("\nStudents");
                Console.WriteLine("==========");
                foreach (var student in context.People.OfType<Student>())
                {
                    Console.WriteLine("\t{0}", student.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
