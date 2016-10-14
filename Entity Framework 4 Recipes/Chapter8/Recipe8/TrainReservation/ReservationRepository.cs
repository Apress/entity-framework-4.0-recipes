using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainReservation
{
    public class ReservationRepository
    {
        private IReservationContext _context;

        public ReservationRepository(IReservationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context is null");
            _context = context;
        }
        public void AddTrain(Train train)
        {
            _context.Trains.AddObject(train);
        }

        public void AddSchedule(Schedule schedule)
        {
            _context.Schedules.AddObject(schedule);
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.AddObject(reservation);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<Schedule> GetActiveSchedulesForTrain(int trainId)
        {
            var schedules = from r in _context.Schedules
                            where r.ArrivalDate.Date >= DateTime.Today && r.TrainId == trainId
                            select r;
            return schedules.ToList();
        }
    }
}
