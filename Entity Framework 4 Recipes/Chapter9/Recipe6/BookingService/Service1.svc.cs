using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using BookingData;
using BookingEntities;
namespace BookingService
{
    public class Service1 : IService1
    {
        public TravelAgent GetAgentWithBookings()
        {
            InsertAgent();
            using (var context = new EFRecipesEntities())
            {
                return context.TravelAgents.Include("Bookings").Single(a => a.Name == "John Tate");
            }
        }

        public void SubmitAgentBookings(TravelAgent agent)
        {
            using (var context = new EFRecipesEntities())
            {
                ValidateAgentBeforeApplyChanges(agent);
                context.TravelAgents.ApplyChanges(agent);
                ValidateAgentAfterApplyChanges(context);
                context.SaveChanges();
            }
        }

        private void ValidateAgentAfterApplyChanges(EFRecipesEntities context)
        {
            var cantDelete = context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted).Any(e => e.Entity is Booking && ((Booking)e.Entity).Paid);
            ValidateCondition(cantDelete, "Can't delete a booking that is paid for.");

            var cantBook = context.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Any(e => e.Entity is Booking && ((Booking)e.Entity).BookingDate.Subtract(DateTime.Today).Days > 20);
            ValidateCondition(cantBook, "Can't book more than 20 days in advance.");
        }

        private void ValidateAgentBeforeApplyChanges(TravelAgent agent)
        {
            var cantAddOrDelete = agent.ChangeTracker.State == ObjectState.Deleted || agent.ChangeTracker.State == ObjectState.Deleted;
            ValidateCondition(cantAddOrDelete, "Can't add or delete an agent.");

            var cantModify = agent.Bookings.Any(b => b.ChangeTracker.State == ObjectState.Modified && b.BookingDate < DateTime.Today);
            ValidateCondition(cantModify, "Can't modify an expired booking.");
        }

        private static void ValidateCondition(bool condition, string message)
        {
            if (condition)
            {
                throw new FaultException<InvalidOperationException>(new InvalidOperationException(message), message);
            }
        }

        private void InsertAgent()
        {
            using (var context = new EFRecipesEntities())
            {
                // delete any previous test data
                context.ExecuteStoreCommand("delete from chapter9.booking");
                context.ExecuteStoreCommand("delete from chapter9.travelagent");

                // inser the test data
                var agent = new TravelAgent { Name = "John Tate" };
                var booking = new Booking {Customer = "Karen Stevens", Paid = false, BookingDate = DateTime.Parse("2/2/2010")};
                agent.Bookings.Add(booking);
                context.TravelAgents.AddObject(agent);
                context.SaveChanges();
            }
        }
    }

}
