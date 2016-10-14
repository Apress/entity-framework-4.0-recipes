using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

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
            using (var context = new EFRecipeEntities())
            {
                context.ExecuteStoreCommand("delete from chapter8.competitor");
                context.ExecuteStoreCommand("delete from chapter8.event");
                context.ExecuteStoreCommand("delete from chapter8.venue");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipeEntities())
            {
                var venue = new Venue { Name = "City Center Hall" };
                var event1 = new Event { Name = "All Star Boxing" };
                event1.Competitors.Add(new Competitor { Name = "Big Joe Green" });
                event1.Competitors.Add(new Competitor { Name = "Terminator Tim" });
                venue.Events.Add(event1);
                context.Venues.AddObject(venue);
                context.SaveChanges();
            }

            using (var context = new EFRecipeEntities())
            {
                foreach (var venue in context.Venues)
                {
                    Console.WriteLine("Venue: {0}", venue.Name);
                    context.LoadProperty(venue, v => v.Events);
                    foreach (var evt in venue.Events)
                    {
                        Console.WriteLine("\tEvent: {0}", evt.Name);
                        Console.WriteLine("\t--- Competitors ---");
                        context.LoadProperty(evt, e => e.Competitors);
                        foreach (var competitor in evt.Competitors)
                        {
                            Console.WriteLine("\t{0}", competitor.Name);
                        }
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Venue
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
        public Venue()
        {
            this.Events = new HashSet<Event>();
        }
    }

    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public ICollection<Competitor> Competitors { get; set; }
        public Event()
        {
            this.Competitors = new HashSet<Competitor>();
        }
    }

    public class Competitor
    {
        public int CompetitorId { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
    }

    public class EFRecipeEntities : ObjectContext
    {
        public EFRecipeEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Venue> venues;
        private ObjectSet<Event> events;
        private ObjectSet<Competitor> competitors;

        public ObjectSet<Venue> Venues
        {
            get { return venues ?? (venues = CreateObjectSet<Venue>());}
        }

        public ObjectSet<Event> Events
        {
            get { return events ?? (events = CreateObjectSet<Event>());}
        }

        public ObjectSet<Competitor> Competitors
        {
            get { return competitors ?? (competitors = CreateObjectSet<Competitor>()); }
        }
    }
}
