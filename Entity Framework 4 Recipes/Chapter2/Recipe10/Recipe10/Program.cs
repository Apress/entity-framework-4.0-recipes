using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe10
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
                context.ExecuteStoreCommand("delete from chapter2.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var fte = new FullTimeEmployee {FirstName = "Jane", LastName = "Doe", Salary = 71500M};
                context.Employees.AddObject(fte);
                fte = new FullTimeEmployee { FirstName = "John", LastName = "Smith", Salary = 62500M };
                context.Employees.AddObject(fte);
                var hourly = new HourlyEmployee { FirstName = "Tom", LastName = "Jones", Wage = 8.75M };
                context.Employees.AddObject(hourly);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("--- All Employees ---");
                foreach (var emp in context.Employees)
                {
                    bool fullTime = emp is HourlyEmployee ? false : true;
                    Console.WriteLine("{0} {1} ({2})", emp.FirstName, emp.LastName, fullTime ? "Full Time" : "Hourly");
                }

                Console.WriteLine("--- Full Time ---");
                foreach (var fte in context.Employees.OfType<FullTimeEmployee>())
                {
                    Console.WriteLine("{0} {1}", fte.FirstName, fte.LastName);
                }

                Console.WriteLine("--- Hourly ---");
                foreach (var hourly in context.Employees.OfType<HourlyEmployee>())
                {
                    Console.WriteLine("{0} {1}", hourly.FirstName, hourly.LastName);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
