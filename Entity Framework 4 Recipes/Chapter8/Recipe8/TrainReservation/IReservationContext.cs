using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace TrainReservation
{
    public interface IReservationContext : IDisposable
    {
        IObjectSet<Train> Trains { get; }
        IObjectSet<Schedule> Schedules { get; }
        IObjectSet<Reservation> Reservations { get; }

        int SaveChanges();
    }
}
