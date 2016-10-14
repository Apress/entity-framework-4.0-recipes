using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
                context.ExecuteStoreCommand("delete from chapter12.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var emp1 = new Employee { Name = "Roger Smith", Salary = 108000M };
                var emp2 = new Employee { Name = "Jane Hall", Salary = 81500M };
                context.Employees.AddObject(emp1);
                context.Employees.AddObject(emp2);
                context.SaveChanges();
                emp1.Salary = emp1.Salary * 1.5M;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("Oops, tried to increase a salary too much!");
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Employees");
                foreach (var emp in context.Employees)
                {
                    Console.WriteLine("{0} makes {1}/year", emp.Name, emp.Salary.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public partial class EFRecipesEntities
    {
        partial void OnContextCreated()
        {
            this.SavingChanges += new EventHandler(EFRecipesEntities_SavingChanges);
        }

        void EFRecipesEntities_SavingChanges(object sender, EventArgs e)
        {
            var entries = this.ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Where(entry => entry.Entity is Employee);
            foreach (var entry in entries)
            {
                var salaryProp = entry.GetModifiedProperties().FirstOrDefault(p => p == "Salary");
                if (salaryProp != null)
                {
                    var originalSalary = Convert.ToDecimal(entry.OriginalValues[salaryProp]);
                    var currentSalary = Convert.ToDecimal(entry.CurrentValues[salaryProp]);
                    if (originalSalary != currentSalary)
                    {
                        if (currentSalary > originalSalary * 1.1M)
                            throw new ApplicationException("Can't increase salary more than 10%");
                    }
                }
            }
        }
    }
}
