using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

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
                context.ExecuteStoreCommand("delete from chapter14.employee");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                using (var scope1 = new TransactionScope())
                {
                    // save, but don't commit
                    var outerEmp = new Employee { Name = "Karen Stanfield" };
                    Console.WriteLine("Outer employee: {0}", outerEmp.Name);
                    context.Employees.AddObject(outerEmp);
                    context.SaveChanges();

                    // second transaction for read uncommitted
                    using (var innerContext = new EFRecipesEntities())
                    {
                        using (var scope2 = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
                        {
                            var innerEmp = innerContext.Employees.First(e => e.Name == "Karen Stanfield");
                            Console.WriteLine("Inner employee: {0}", innerEmp.Name);
                            scope1.Complete();
                            scope2.Complete();
                        }
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
