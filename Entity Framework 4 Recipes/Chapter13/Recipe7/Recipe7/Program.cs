using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data.Objects;

namespace Recipe7
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
                context.ExecuteStoreCommand("delete from chapter13.paycheck");
                context.ExecuteStoreCommand("delete from chapter13.associate");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var a1 = new Associate { Name = "Robert Stevens", City = "Raytown" };
                a1.Paychecks.Add(new Paycheck { PayDate = DateTime.Parse("3/1/10"), Gross = 1802.83M });
                a1.Paychecks.Add(new Paycheck { PayDate = DateTime.Parse("3/15/10"), Gross = 1924.91M });
                var a2 = new Associate { Name = "Karen Thorp", City = "Gladstone" };
                a2.Paychecks.Add(new Paycheck { PayDate = DateTime.Parse("3/1/10"), Gross = 2102.34M });
                a2.Paychecks.Add(new Paycheck { PayDate = DateTime.Parse("3/15/10"), Gross = 1992.18M });
                context.Associates.AddObject(a1);
                context.Associates.AddObject(a2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Stopwatch watch = new Stopwatch();
                long totalTicks = 0;

                // warm things up
                context.Associates.Where(a => a.Name.StartsWith("Karen")).ToList();

                // query gets compiled each time
                for (int i = 0; i < 10; i++)
                {
                    watch.Restart();
                    context.Associates.Where(a => a.Name.StartsWith("Karen")).ToList();
                    watch.Stop();
                    totalTicks += watch.ElapsedTicks;
                    Console.WriteLine("Not Compiled: {0}", watch.ElapsedTicks.ToString());
                }
                Console.WriteLine("Average ticks without compiling: {0}", (totalTicks / 10).ToString());
                Console.WriteLine("");

                // compile the query just once and re-use
                var query = CompiledQuery.Compile<EFRecipesEntities, IQueryable<Associate>>(ctx =>
                    from a in ctx.Associates
                    where a.Name.StartsWith("Karen")
                    select a);
                totalTicks = 0;
                for (int i = 0; i < 10; i++)
                {
                    watch.Restart();
                    query(context).ToList();
                    watch.Stop();
                    totalTicks += watch.ElapsedTicks;
                    Console.WriteLine("Compiled: {0}", watch.ElapsedTicks.ToString());
                }
                Console.WriteLine("Average ticks with compiling: {0}", (totalTicks / 10).ToString());
            }

            using (var context = new EFRecipesEntities())
            {
                // an example of returning an anonymous type
                var list = CompiledQuery.Compile((EFRecipesEntities ctx) =>
                    from a in ctx.Associates
                    select new
                        {
                            Name = a.Name,
                            TotalPay = a.Paychecks.Sum(p => p.Gross)
                        });
                var everyone = list(context).ToList();
            }

            using (var context = new EFRecipesEntities())
            {
                // an example of composing compiled and non-compiled queries
                var query = CompiledQuery.Compile<EFRecipesEntities, string, IQueryable<Associate>>((ctx, city) =>
                    ctx.Associates.Where(c => c.City == city));
                var highlyPaid = from a in query(context,"Raytown")
                                 where a.Paychecks.Average(p => p.Gross) > 1500M
                                 select a;
                var associates = highlyPaid.ToList();
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
