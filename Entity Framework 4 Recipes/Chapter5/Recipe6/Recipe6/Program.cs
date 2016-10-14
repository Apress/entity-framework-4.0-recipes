using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe6
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
                context.ExecuteStoreCommand("delete from chapter5.reservation");
                context.ExecuteStoreCommand("delete from chapter5.executivesuite");
                context.ExecuteStoreCommand("delete from chapter5.room");
                context.ExecuteStoreCommand("delete from chapter5.hotel");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var hotel = new Hotel { Name = "Grand Seasons Hotel" };
                var r101 = new Room { Rate = 79.95M, Hotel = hotel };
                var es201 = new ExecutiveSuite { Rate = 179.95M, Hotel = hotel };
                var es301 = new ExecutiveSuite { Rate = 299.95M, Hotel = hotel };
                var res1 = new Reservation { StartDate = DateTime.Parse("3/12/2010"), EndDate = DateTime.Parse("3/14/2010"), ContactName = "Roberta Jones", Room = es301 };
                var res2 = new Reservation { StartDate = DateTime.Parse("1/18/2010"), EndDate = DateTime.Parse("1/28/2010"), ContactName = "Bill Meyers", Room = es301 };
                var res3 = new Reservation { StartDate = DateTime.Parse("2/5/2010"), EndDate = DateTime.Parse("2/6/2010"), ContactName = "Robin Rosen", Room = r101 };
                context.Hotels.AddObject(hotel);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // assume we have an instance of hotel
                var hotel = context.Hotels.First();

                var rooms = hotel.Rooms.CreateSourceQuery().Include("Reservations").Where(r => r is ExecutiveSuite && r.Reservations.Any()).OrderBy(r => r.Rate);
                Console.WriteLine("Executive Suites for {0} with reservations", hotel.Name);
                hotel.Rooms.Attach(rooms);
                foreach (var room in hotel.Rooms)
                {
                    Console.WriteLine("\nExecutive Suite {0} is {1} per night", room.RoomId.ToString(), room.Rate.ToString("C"));
                    Console.WriteLine("Current reservations are:");
                    foreach (var res in room.Reservations.OrderBy(r => r.StartDate))
                    {
                        Console.WriteLine("\t{0} thru {1} ({2})", res.StartDate.ToShortDateString(), res.EndDate.ToShortDateString(), res.ContactName);
                    }
                }
            }

            // alternative approach using .OfType<>()
            Console.WriteLine("\n--- Alternative approach using .OfType<>() ---");
            using (var context = new EFRecipesEntities())
            {
                // assume we have an instance of hotel
                var hotel = context.Hotels.First();

                var rooms = hotel.Rooms.CreateSourceQuery().Include("Reservations").OfType<ExecutiveSuite>().Where(r => r.Reservations.Any()).OrderBy(r => r.Rate);
                Console.WriteLine("Executive Suites for {0} with reservations", hotel.Name);
                hotel.Rooms.Attach(rooms);
                foreach (var room in hotel.Rooms)
                {
                    Console.WriteLine("\nExecutive Suite {0} is {1} per night", room.RoomId.ToString(), room.Rate.ToString("C"));
                    Console.WriteLine("Current reservations are:");
                    foreach (var res in room.Reservations.OrderBy(r => r.StartDate))
                    {
                        Console.WriteLine("\t{0} thru {1} ({2})", res.StartDate.ToShortDateString(), res.EndDate.ToShortDateString(), res.ContactName);
                    }
                }
            }



            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
