using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe5
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
                context.ExecuteStoreCommand("delete from chapter5.employee");
                context.ExecuteStoreCommand("delete from chapter5.department");
                context.ExecuteStoreCommand("delete from chapter5.company");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var company = new Company { Name = "Acme Products" };
                var acc = new Department { Name = "Accounting", Company = company };
                var ship = new Department { Name = "Shipping", Company = company };
                var emp1 = new Employee { Name = "Jill Carpenter", Department = acc };
                var emp2 = new Employee { Name = "Steven Hill", Department = ship };
                context.Employees.AddObject(emp1);
                context.Employees.AddObject(emp2);
                context.SaveChanges();
            }

            // first approach
            using (var context = new EFRecipesEntities())
            {
                // assume we already have an employee
                var jill = context.Employees.Where(o => o.Name == "Jill Carpenter").First();

                // now get Jill's department and company
                var results = context.Employees.Include("Department.Company").Where(o => o.EmployeeId == jill.EmployeeId).First<Employee>();
                Console.WriteLine("{0} works in {1} for {2}", jill.Name, jill.Department.Name, jill.Department.Company.Name);
            }

            // more efficient, does not retrieve employee again
            using (var context = new EFRecipesEntities())
            {
                // assume we already have an employee
                var jill = context.Employees.Where(o => o.Name == "Jill Carpenter").First();

                var moreResults = jill.DepartmentReference.CreateSourceQuery().Include("Company").First();
                context.Attach(moreResults);
                Console.WriteLine("{0} works in {1} for {2}", jill.Name, jill.Department.Name, jill.Department.Company.Name);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
