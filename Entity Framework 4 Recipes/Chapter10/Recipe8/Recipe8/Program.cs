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
                context.ExecuteStoreCommand("delete from chapter10.athlete");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Athletes.AddObject(new Athlete { Name = "Nancy Steward", Height = 167, Weight = 53 });
                context.Athletes.AddObject(new Athlete { Name = "Rob Achers", Height = 170, Weight = 77 });
                context.Athletes.AddObject(new Athlete { Name = "Chuck Sanders", Height = 171, Weight = 82 });
                context.Athletes.AddObject(new Athlete { Name = "Nancy Rodgers", Height = 166, Weight = 59 });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // do a delete and an update
                var all = context.Athletes;
                context.DeleteObject(all.Where(o => o.Name == "Nancy Steward").First());
                all.Where(o => o.Name == "Rob Achers").First().Weight = 80;
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("All Athletes");
                Console.WriteLine("============");
                foreach (var athlete in context.Athletes)
                {
                    Console.WriteLine("{0} weighs {1} Kg and is {2} cm in height", athlete.Name, athlete.Weight, athlete.Height);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
