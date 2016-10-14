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
                context.ExecuteStoreCommand("delete from chapter10.Employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var emp1 = new Employee { Name = "Lisa Jefferies", Address = new EmployeeAddress { Address = "100 E. Main", City = "Fort Worth", State = "TX", ZIP = "76106" } };
                var emp2 = new Employee { Name = "Robert Jones", Address = new EmployeeAddress { Address = "3920 South Beach", City = "Fort Worth", State = "TX", ZIP = "76102" } };
                var emp3 = new Employee { Name = "Steven Chue", Address = new EmployeeAddress { Address = "129 Barker", City = "Euless", State = "TX", ZIP = "76092" } };
                var emp4 = new Employee { Name = "Karen Stevens", Address = new EmployeeAddress { Address = "108 W. Parker", City = "Fort Worth", State = "TX", ZIP = "76102" } };
                context.AddToEmployees(emp1);
                context.AddToEmployees(emp2);
                context.AddToEmployees(emp3);
                context.AddToEmployees(emp4);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Employee addresses in Fort Worth, TX");
                foreach (var address in context.GetEmployeeAddresses("Fort Worth"))
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}", address.Address, address.City, address.State, address.ZIP);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
