using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

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
                context.ExecuteStoreCommand("delete from chapter8.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Employees.AddObject(new Employee { Name = new Name { FirstName = "Annie", LastName = "Oakley" }, Email = "aoakley@wildwestshow.com" });
                context.Employees.AddObject(new Employee { Name = new Name { FirstName = "Bill", LastName = "Jordan" }, Email = "BJordan@wildwestshow.com" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var employee in context.Employees.OrderBy(e => e.Name.LastName))
                {
                    Console.WriteLine("{0}, {1} email: {2}", employee.Name.LastName, employee.Name.FirstName, employee.Email);
                }
            }

            int id = 0;
            using (var context = new EFRecipesEntities())
            {
                var emp = context.Employees.Where(e => e.Name.FirstName.StartsWith("Bill")).FirstOrDefault();
                id = emp.EmployeeId;
            }

            using (var context = new EFRecipesEntities())
            {
                var empDelete = new Employee { EmployeeId = id, Name = new Name { FirstName = string.Empty, LastName = string.Empty } };
                context.Employees.Attach(empDelete);
                context.Employees.DeleteObject(empDelete);
                context.SaveChanges();
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public Name Name { get; set; }
    }

    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Employee> employees;

        public ObjectSet<Employee> Employees
        {
            get { return employees ?? (employees = CreateObjectSet<Employee>()); }
        }
    }
}
