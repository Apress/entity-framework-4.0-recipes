using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter8.violation");
                context.ExecuteStoreCommand("delete from chapter8.ticket");
                context.ExecuteStoreCommand("delete from chapter8.vehicle");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var vh1 = new Vehicle { LicenseNo = "BR-549" };
                var t1 = new Ticket { IssueDate = DateTime.Parse("4/18/10") };
                var v1 = new Violation { Description = "20 MPH over the speed limit", Amount = 125M };
                var v2 = new Violation { Description = "Broken tail light", Amount = 50M };
                t1.Violations.Add(v1);
                t1.Violations.Add(v2);
                t1.Vehicle = vh1;
                context.Tickets.AddObject(t1);
                var vh2 = new Vehicle { LicenseNo = "XJY-902" };
                var t2 = new Ticket { IssueDate = DateTime.Parse("4/20/10") };
                var v3 = new Violation { Description = "Parking in a no parking zone", Amount = 35M };
                t2.Violations.Add(v3);
                t2.Vehicle = vh2;
                context.Tickets.AddObject(t2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                context.ContextOptions.LazyLoadingEnabled = true;
                foreach (var ticket in context.Tickets)
                {
                    Console.WriteLine(" Ticket: {0}, Total Cost: {1}", ticket.TicketId.ToString(), ticket.Violations.Sum(v => v.Amount).ToString("C"));
                    foreach (var violation in ticket.Violations)
                    {
                        Console.WriteLine("\t{0}", violation.Description);
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Ticket
    {
        public int TicketId { get; set; }
        public int VehicleId { get; set; }
        public DateTime IssueDate { get; set; }
        public virtual Vehicle Vehicle {get; set;}
        public virtual ICollection<Violation> Violations {get; private set;}
        public Ticket()
        {
            this.Violations = new HashSet<Violation>();
        }
    }

    public class Vehicle
    {
        public int VehicleId {get; set;}
        public string LicenseNo {get; set;}
    }

    public class Violation
    {
        public int ViolationId { get; set; }
        public int TicketId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
        }

        private ObjectSet<Ticket> tickets;
        private ObjectSet<Violation> violations;
        private ObjectSet<Vehicle> vehicles;

        public ObjectSet<Ticket> Tickets
        {
            get { return tickets ?? (tickets = CreateObjectSet<Ticket>()); }
        }

        public ObjectSet<Violation> Violations
        {
            get { return violations ?? (violations = CreateObjectSet<Violation>()); }
        }

        public ObjectSet<Vehicle> Vehicles
        {
            get { return vehicles ?? (vehicles = CreateObjectSet<Vehicle>()); }
        }
    }
}
