using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe4
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
                context.ExecuteStoreCommand("delete from chapter5.event");
                context.ExecuteStoreCommand("delete from chapter5.club");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var club = new Club { Name = "Star City Chess Club", City = "New York" };
                context.Clubs.AddObject(club);
                new Event { EventName = "Mid Cities Tournament", EventDate = DateTime.Parse("1/09/2010"), Club = club };
                new Event { EventName = "State Finals Tournament", EventDate = DateTime.Parse("2/12/2010"), Club = club };
                new Event { EventName = "Winter Classic", EventDate = DateTime.Parse("12/18/2009"), Club = club };
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var events = from ev in context.Events
                             where ev.Club.City == "New York"
                             group ev by ev.Club into g
                             select g.FirstOrDefault(e1 => e1.EventDate == g.Min(evt => evt.EventDate));
                var e = ((ObjectQuery<Event>)events).Include("Club").First();
                Console.WriteLine("The next New York club event is:");
                Console.WriteLine("\tEvent: {0}", e.EventName);
                Console.WriteLine("\tDate: {0}", e.EventDate.ToShortDateString());
                Console.WriteLine("\tClub: {0}", e.Club.Name);
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
