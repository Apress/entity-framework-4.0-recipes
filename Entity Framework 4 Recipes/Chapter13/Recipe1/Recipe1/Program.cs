using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter13.hourlyemployee");
                context.ExecuteStoreCommand("delete from chapter13.salariedemployee");
                context.ExecuteStoreCommand("delete from chapter13.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Employees.AddObject(new SalariedEmployee { Name = "Robin Rosen", Salary = 89900M });
                context.Employees.AddObject(new HourlyEmployee { Name = "Steven Fuller", Rate = 11.50M });
                context.Employees.AddObject(new HourlyEmployee { Name = "Karen Steele", Rate = 12.95m });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // a typical way to get Steven Fuller's entity
                var emp1 = context.Employees.Single(e => e.Name == "Steven Fuller");
                Console.WriteLine("{0}'s rate is: {1} per hour", emp1.Name, ((HourlyEmployee)emp1).Rate.ToString("C"));

                // slightly more efficient way if we know that Steven is an HourlyEmployee
                var emp2 = context.Employees.OfType<HourlyEmployee>().Single(e => e.Name == "Steven Fuller");
                Console.WriteLine("{0}'s rate is: {1} per hour", emp2.Name, ((HourlyEmployee)emp2).Rate.ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
