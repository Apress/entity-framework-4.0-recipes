using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Recipe2
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
                context.ExecuteStoreCommand("delete from chapter14.agent");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Agents.AddObject(new Agent { Name = "Phillip Marlowe", Phone = "202 555-1212" });
                context.Agents.AddObject(new Agent { Name = "Janet Rooney", Phone = "913 876-5309" });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // change the phone numbers
                var agent1 = context.Agents.Where(a => a.Name == "Janet Rooney").Single();
                var agent2 = context.Agents.Where(a => a.Name == "Phillip Marlowe").Single();
                agent1.Phone = "817 353-4458";
                context.SaveChanges();

                // update the other agent's number out-of-band
                context.ExecuteStoreCommand(@"update Chapter14.agent set Phone = '817 294-6059' where name = 'Phillip Marlowe'");

                // now change it using the model
                agent2.Phone = "817 906-2212";
                try
                {
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Exception caught updating phone number: {0}", ex.Message);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("-- All Agents --");
                foreach (var agent in context.Agents)
                {
                    Console.WriteLine("Agent: {0}, Phone: {1}", agent.Name, agent.Phone);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
