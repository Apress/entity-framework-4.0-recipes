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
                context.ExecuteStoreCommand("delete from chapter6.Instructor");
                context.ExecuteStoreCommand("delete from chapter6.PRincipal");
                context.ExecuteStoreCommand("delete from chapter6.staff");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var principal = new Principal { Name = "Robbie Smith", Bonus = 3500M, Salary = 48000M };
                var instructor = new Instructor { Name = "Joan Carlson", Salary = 39000M };
                context.Staffs.AddObject(principal);
                context.Staffs.AddObject(instructor);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Principals");
                Console.WriteLine("==========");
                foreach (var p in context.Staffs.OfType<Principal>())
                {
                    Console.WriteLine("\t{0}, Salary: {1}, Bonus: {2}", p.Name, p.Salary.ToString("C"), p.Bonus.ToString("C"));
                }
                Console.WriteLine("Instructors");
                Console.WriteLine("===========");
                foreach (var i in context.Staffs.OfType<Instructor>())
                {
                    Console.WriteLine("\t{0}, Salary: {1}", i.Name, i.Salary.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
