using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe12
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
                context.ExecuteStoreCommand("delete from chapter2.agent");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var name1 = new Name { FirstName = "Robin", LastName = "Rosen" };
                var name2 = new Name { FirstName = "Alex", LastName = "St. James" };
                var address1 = new Address { AddressLine1 = "510 N. Grant", AddressLine2 = "Apt. 8", City = "Raytown", State = "MO", ZIPCode = "64133" };
                var address2 = new Address { AddressLine1 = "222 Baker St.", AddressLine2 = "Apt.22B", City = "Raytown", State = "MO", ZIPCode = "64133" };
                context.Agents.AddObject(new Agent { Name = name1, Address = address1 });
                context.Agents.AddObject(new Agent {Name = name2, Address = address2});
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Agents");
                foreach (var agent in context.Agents)
                {
                    Console.WriteLine("{0} {1}", agent.Name.FirstName, agent.Name.LastName);
                    Console.WriteLine("{0}", agent.Address.AddressLine1);
                    Console.WriteLine("{0}", agent.Address.AddressLine2);
                    Console.WriteLine("{0}, {1} {2}", agent.Address.City, agent.Address.State, agent.Address.ZIPCode);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
