using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter11.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Employees.AddObject(new Employee { FirstName = "Jill", LastName = "Robins", Birthdate = DateTime.Parse("3/2/1976") });
                context.Employees.AddObject(new Employee { FirstName = "Michael", LastName = "Kirk", Birthdate = DateTime.Parse("4/12/1985") });
                context.Employees.AddObject(new Employee { FirstName = "Karen", LastName = "Stanford", Birthdate = DateTime.Parse("6/18/1963") });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Query using eSql");
                var esql = @"Select EFRecipesModel.FullName(e) as Name, EFRecipesModel.Age(e) as Age from EFRecipesEntities.Employees as e";
                var emps = context.CreateQuery<DbDataRecord>(esql);
                foreach (var emp in emps)
                {
                    Console.WriteLine("Employee: {0}, Age: {1}", emp["Name"], emp["Age"]);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("\nQuery using LINQ");
                var emps = from e in context.Employees
                           select new
                           {
                               Name = MyFunctions.FullName(e),
                               Age = MyFunctions.Age(e)
                           };
                foreach (var emp in emps)
                {
                    Console.WriteLine("Employee: {0}, Age: {1}", emp.Name, emp.Age.ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class MyFunctions
    {
        [EdmFunction("EFRecipesModel", "FullName")]
        public static string FullName(Employee employee)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        [EdmFunction("EFRecipesModel", "Age")]
        public static int Age(Employee employee)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}
