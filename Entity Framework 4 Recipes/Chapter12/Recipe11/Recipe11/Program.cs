using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe11
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
                context.ExecuteStoreCommand("delete from chapter12.parkingticket");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ParkingTickets.AddObject(new ParkingTicket { Amount = 132.0M, Paid = false });
                context.ParkingTickets.AddObject(new ParkingTicket { Amount = 255.0M, Paid = false });
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var ticket in context.ParkingTickets)
                {
                    Console.WriteLine("Ticket: {0}", ticket.TicketId);
                    Console.WriteLine("Date: {0}", ticket.CreateDate.ToShortDateString());
                    Console.WriteLine("Amount: {0}", ticket.Amount.ToString("C"));
                    Console.WriteLine("Paid: {0}", ticket.PaidDate.HasValue ? ticket.PaidDate.Value.ToShortDateString() : "Not Paid");
                    Console.WriteLine();
                    ticket.Paid = true; // just paid ticket!
                }

                // save all those Paid flags
                context.SaveChanges();
                foreach (var ticket in context.ParkingTickets)
                {
                    Console.WriteLine("Ticket: {0}", ticket.TicketId);
                    Console.WriteLine("Date: {0}", ticket.CreateDate.ToShortDateString());
                    Console.WriteLine("Amount: {0}", ticket.Amount.ToString("C"));
                    Console.WriteLine("Paid: {0}", ticket.PaidDate.HasValue ? ticket.PaidDate.Value.ToShortDateString() : "Not Paid");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
