using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Recipe8
{
    public class ReservationRepository
    {
        private EFRecipesEntities context;

        public ReservationRepository()
        {
            this.context = new EFRecipesEntities();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public List<Reservation> GetReservations(string sort, int startRowIndex, int maximumRows)
        {
            return this.context.Reservations.Include("Hotel").OrderBy("it." + (sort == string.Empty ? "Name" : sort)).Skip(startRowIndex).Take(maximumRows).ToList();
        }

        public int ReservationCount()
        {
            return this.context.Reservations.Count();
        }

        public void Insert(Reservation reservation)
        {
            this.context.Reservations.AddObject(reservation);
            context.SaveChanges();
        }

        public void Update(Reservation reservation)
        {
            this.context.Reservations.Attach(reservation);
            this.context.ObjectStateManager.ChangeObjectState(reservation, EntityState.Modified);
            this.context.SaveChanges();
        }

        public void Delete(Reservation reservation)
        {
            this.context.Reservations.Attach(reservation);
            this.context.Reservations.DeleteObject(reservation);
            this.context.SaveChanges();
        }
    }
}