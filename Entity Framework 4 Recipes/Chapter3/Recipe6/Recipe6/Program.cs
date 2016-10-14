using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

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
                context.ExecuteStoreCommand("delete from chapter3.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Employees.AddObject(new Employee { Name = "Robin Rosen", YearsWorked = 3 });
                context.Employees.AddObject(new Employee { Name = "John Hancock" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Employees (using LINQ)");
                var employees = from e in context.Employees
                                select new {Name = e.Name, YearsWorked = e.YearsWorked ?? 0};
                foreach(var employee in employees)
                {
                    Console.WriteLine("{0}, years worked: {1}",employee.Name, employee.YearsWorked);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Employees (using ESQL)");
                string esql = @"select 
                                  e.Name,
                                  case when e.YearsWorked is null then 0
                                       else e.YearsWorked
                                  end as YearsWorked
                                from Employees as e";
                var employees = context.CreateQuery<DbDataRecord>(esql);
                foreach (var employee in employees)
                {
                    Console.WriteLine("{0}, years worked: {1}", employee.GetString(0), employee.GetInt32(1).ToString());
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Employees (using ESQL w/named constructor)");
                string esql = @"select value Recipe6.Employee(e.EmployeeId, 
                                  e.Name,
                                  case when e.YearsWorked is null then 0
                                       else e.YearsWorked end) 
                                from Employees as e";
                var employees = context.CreateQuery<Employee>(esql);
                foreach(var employee in employees)
                {
                    Console.WriteLine("{0}, years worked: {1}",employee.Name, employee.YearsWorked.ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
