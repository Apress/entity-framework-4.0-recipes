using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter11.event");
                context.ExecuteStoreCommand("delete from chapter11.sponsor");
                context.ExecuteStoreCommand("delete from chapter11.sponsortype");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var platst = new SponsorType { Description = "Platinum" };
                var goldst = new SponsorType { Description = "Gold" };
                var sp1 = new Sponsor { Name = "Rex's Auto Body Shop", SponsorType = goldst };
                var sp2 = new Sponsor { Name = "Midtown Eye Care Center", SponsorType = platst };
                var sp3 = new Sponsor { Name = "Tri-Cities Ford", SponsorType = platst };
                var ev1 = new Event { Name = "OctoberFest", Sponsor = sp1 };
                var ev2 = new Event { Name = "Concerts in the Park", Sponsor = sp2 };
                var ev3 = new Event { Name = "11th Street Art Festival", Sponsor = sp3 };
                context.Events.AddObject(ev1);
                context.Events.AddObject(ev2);
                context.Events.AddObject(ev3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Events with Platinum Sponsors");
                Console.WriteLine("=============================");
                var esql = @"select value e from EFRecipesEntities.Events as e 
                             where ref(e.Sponsor) in (EFRecipesModel.PlatinumSponsors())";
                var events = context.CreateQuery<Event>(esql);
                foreach (var ev in events)
                {
                    Console.WriteLine(ev.Name);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
