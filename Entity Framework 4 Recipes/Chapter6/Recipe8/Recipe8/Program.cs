using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe8
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
                context.ExecuteStoreCommand("delete from chapter6.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var hourly = new HourlyEmployee { Name = "Will Smith", Hours = 39, Rate = 7.75M };
                var salaried = new SalariedEmployee { Name = "JoAnn Woodland", Salary = 65400M };
                var commissioned = new CommissionedEmployee { Name = "Joel Clark", Salary = 32500M, Commission = 20M };
                context.Employees.AddObject(hourly);
                context.Employees.AddObject(salaried);
                context.Employees.AddObject(commissioned);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("All Employees");
                Console.WriteLine("=============");
                foreach (var emp in context.Employees)
                {
                    if (emp is HourlyEmployee)
                        Console.WriteLine("{0} Hours = {1}, Rate = {2}/hour", emp.Name, ((HourlyEmployee)emp).Hours.Value.ToString(), ((HourlyEmployee)emp).Rate.Value.ToString("C"));
                    else if (emp is CommissionedEmployee)
                        Console.WriteLine("{0} Salary = {1}, Commission = {2}%", emp.Name, ((CommissionedEmployee)emp).Salary.Value.ToString("C"), ((CommissionedEmployee)emp).Commission.ToString());
                    else if (emp is SalariedEmployee)
                        Console.WriteLine("{0} Salary = {1}", emp.Name, ((SalariedEmployee)emp).Salary.Value.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
