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
                context.ExecuteStoreCommand("delete from chapter7.task");
                context.ExecuteStoreCommand("delete from chapter7.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var employee1 = new Employee {EmployeeNumber = 629, Name = "Robin Rosen", Salary = 106000M };
                var employee2 = new Employee {EmployeeNumber = 147, Name = "Bill Moore", Salary = 62500M };
                var task1 = new Task { Description = "Report 3rd Qtr Accounting" };
                var task2 = new Task { Description = "Forecast 4th Qtr Sales" };
                var task3 = new Task { Description = "Prepare Sales Tax Report" };

                // use AddObject() on the Employees entity set
                context.Employees.AddObject(employee1);

                // add two new tasks to the employee1's tasks
                employee1.Tasks.Add(task1);
                employee1.Tasks.Add(task2);

                // add a task to the employee and use
                // AddObject() to add the task to the object context
                employee2.Tasks.Add(task3);
                context.Tasks.AddObject(task3);

                // persist all of these to the database
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var employee in context.Employees)
                {
                    Console.WriteLine("Employee: {0}'s Tasks", employee.Name);
                    foreach (var task in employee.Tasks)
                    {
                        Console.WriteLine("\t{0}", task.Description);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
