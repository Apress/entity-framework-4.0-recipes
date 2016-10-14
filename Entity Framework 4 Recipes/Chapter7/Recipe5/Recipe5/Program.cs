using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;

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
                context.ExecuteStoreCommand("delete from chapter7.servicecall");
                context.ExecuteStoreCommand("delete from chapter7.technician");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var tech1 = new Technician { Name = "Julie Kerns" };
                var tech2 = new Technician { Name = "Robert Allison" };
                context.ServiceCalls.AddObject(new ServiceCall { ContactName = "Robin Rosen", Issue = "Can't get satellite signal.", Technician = tech1 });
                context.ServiceCalls.AddObject(new ServiceCall { ContactName = "Phillip Marlowe", Issue = "Channel not available", Technician = tech2 });

                // now get the entities we've added
                foreach (var tech in context.ObjectStateManager.GetEntities<Technician>())
                {
                    Console.WriteLine("Technician: {0}", tech.Name);
                    foreach (var call in tech.ServiceCalls)
                    {
                        Console.WriteLine("\tService Call: Contact {0} about {1}", call.ContactName, call.Issue);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public static class StateManagerExtensions
    {
        public static IEnumerable<T> GetEntities<T>(this ObjectStateManager manager)
        {
            var entities = manager.GetObjectStateEntries(~EntityState.Detached).Where(entry => !entry.IsRelationship && entry.Entity != null).Select(entry => entry.Entity).OfType<T>();
            return entities;
        }
    }
}
