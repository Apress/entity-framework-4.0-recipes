using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BookingClient.ServiceReference1;
using BookingEntities;
namespace BookingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new Service1Client())
            {
                var agent = client.GetAgentWithBookings();
                agent.Bookings.Add(new Booking {BookingDate = DateTime.Today.AddDays(5), Customer = "Jan Thomas", Paid = true});
                agent.Name = "John Parker";

                var bookings = agent.Bookings.Where(b => b.Customer == "Karen Stevens").ToList();
                foreach (var booking in bookings)
                {
                    booking.MarkAsDeleted();
                }
                client.SubmitAgentBookings(agent);
            }
        }
    }
}
