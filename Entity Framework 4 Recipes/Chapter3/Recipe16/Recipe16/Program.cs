using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Recipe16
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
                context.ExecuteStoreCommand("delete from chapter3.associatesalary");
                context.ExecuteStoreCommand("delete from chapter3.associate");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var assoc1 = new Associate { Name = "Janis Roberts" };
                var assoc2 = new Associate { Name = "Kevin Hodges" };
                var assoc3 = new Associate { Name = "Bill Jordan" };
                var salary1 = new AssociateSalary { Salary = 39500M, SalaryDate = DateTime.Parse("8/14/09") };
                var salary2 = new AssociateSalary { Salary = 41900M, SalaryDate = DateTime.Parse("2/5/10") };
                var salary3 = new AssociateSalary { Salary = 33500M, SalaryDate = DateTime.Parse("10/08/09") };
                assoc2.AssociateSalaries.Add(salary1);
                assoc2.AssociateSalaries.Add(salary2);
                assoc3.AssociateSalaries.Add(salary3);
                context.Associates.AddObject(assoc1);
                context.Associates.AddObject(assoc2);
                context.Associates.AddObject(assoc3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Using LINQ...");
                var allHistory = from a in context.Associates
                                 from ah in a.AssociateSalaries.DefaultIfEmpty()
                                 orderby a.Name
                                 select new
                                 {
                                     Name = a.Name,
                                     Salary = (decimal ?) ah.Salary,
                                     Date = (DateTime ?) ah.SalaryDate 
                                 };
                Console.WriteLine("Associate Salary History");
                foreach (var history in allHistory)
                {
                    if (history.Salary.HasValue)
                        Console.WriteLine("{0} Salary on {1} was {2}", history.Name, history.Date.Value.ToShortDateString(), history.Salary.Value.ToString("C"));
                    else
                        Console.WriteLine("{0} --", history.Name);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("\nUsing Entity SQL...");
                var esql = @"select a.Name, h.Salary, h.SalaryDate
                             from Associates as a outer apply a.AssociateSalaries as h order by a.Name";
                var allHistory = context.CreateQuery<DbDataRecord>(esql);
                foreach (var history in allHistory)
                {
                    if (history["Salary"] != DBNull.Value)
                        Console.WriteLine("{0} Salary on {1:d} was {2:c}", history["Name"], history["SalaryDate"], history["Salary"]);
                    else
                        Console.WriteLine("{0} --",history["Name"]);
                }
            }
            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
