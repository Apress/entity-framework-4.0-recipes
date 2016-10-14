using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;

namespace TrainReservation
{
    public partial class Reservation : IValidate
    {
        public void Validate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
                if (Schedule.Reservations.Count(r => r.ReservationId != ReservationId && r.Passenger == this.Passenger) > 0)
                    throw new InvalidOperationException("Reservation for the passenger already exists");
            }
        }
    }

    public partial class Schedule : IValidate
    {
        public void Validate(ChangeAction action)
        {
            if (action == ChangeAction.Insert)
            {
                if (ArrivalDate < DepartureDate)
                {
                    throw new InvalidOperationException("Arrival date cannot be before departure date");
                }

                if (LeavesFrom == ArrivesAt)
                {
                    throw new InvalidOperationException("Can't leave from and arrive at the same location");
                }
            }
        }
    }

    public partial class EFRecipesEntities
    {
        public override int SaveChanges(SaveOptions options)
        {
            this.DetectChanges();
            var entries = from e in this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted)
                          where (e.IsRelationship == false) && (e.Entity != null) && (e.Entity is IValidate)
                          select e;
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((IValidate)entry.Entity).Validate(ChangeAction.Insert);
                        break;
                    case EntityState.Modified:
                        ((IValidate)entry.Entity).Validate(ChangeAction.Update);
                        break;
                    case EntityState.Deleted:
                        ((IValidate)entry.Entity).Validate(ChangeAction.Delete);
                        break;
                }
            }
            return base.SaveChanges(options & ~SaveOptions.DetectChangesBeforeSave);
        }
    }
}
