using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter6.EventOrganizer");
                context.ExecuteStoreCommand("delete from chapter6.Event");
                context.ExecuteStoreCommand("delete from chapter6.Organizer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var org = new Organizer { Name = "Community Charity" };
                var evt = new Event { Name = "Fundraiser" };
                org.Events.Add(evt);
                context.Organizers.AddObject(org);
                org = new Organizer { Name = "Boy Scouts" };
                evt = new Event { Name = "Eagle Scout Dinner" };
                org.Events.Add(evt);
                context.Organizers.AddObject(org);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var evsorg1 = from ev in context.Events
                             from organizer in ev.Organizers
                             select new { ev.EventId, organizer.OrganizerId };
                Console.WriteLine("Using nested from clauses...");
                foreach (var pair in evsorg1)
                {
                    Console.WriteLine("EventId {0}, OrganizerId {1}", pair.EventId.ToString(), pair.OrganizerId.ToString());
                }

                var evsorg2 = context.Events.SelectMany(e => e.Organizers, (ev, org) => new { ev.EventId, org.OrganizerId });
                Console.WriteLine("\nUsing SelectManay()");
                foreach (var pair in evsorg2)
                {
                    Console.WriteLine("EventId {0}, OrganizerId {1}", pair.EventId.ToString(), pair.OrganizerId.ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
