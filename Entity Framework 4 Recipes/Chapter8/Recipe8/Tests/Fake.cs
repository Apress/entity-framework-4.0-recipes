using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainReservation;
using System.Data.Objects;
namespace Tests
{
    public class FakeObjectSet<T> : IObjectSet<T> where T : class
    {
        HashSet<T> _data;
        IQueryable _query;

        public FakeObjectSet()
            : this(new List<T>())
        {
        }

        public FakeObjectSet(IEnumerable<T> initialData)
        {
            _data = new HashSet<T>(initialData);
            _query = _data.AsQueryable();
        }

        public void Add(T item)
        {
            _data.Add(item);
        }

        public void AddObject(T item)
        {
            _data.Add(item);
        }

        public void Remove(T item)
        {
            _data.Remove(item);
        }

        public void DeleteObject(T item)
        {
            _data.Remove(item);
        }

        public void Attach(T item)
        {
            _data.Add(item);
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }

    public class FakeReservationContext : IReservationContext, IDisposable
    {
        private IObjectSet<Train> trains;
        private IObjectSet<Schedule> schedules;
        private IObjectSet<Reservation> reservations;
        public FakeReservationContext()
        {
            trains = new FakeObjectSet<Train>();
            schedules = new FakeObjectSet<Schedule>();
            reservations = new FakeObjectSet<Reservation>();
        }

        public IObjectSet<Train> Trains
        {
            get { return trains; }
        }

        public IObjectSet<Schedule> Schedules
        {
            get { return schedules; }
        }

        public IObjectSet<Reservation> Reservations
        {
            get { return reservations; }
        }

        public int SaveChanges()
        {
            foreach (var schedule in Schedules.Cast<IValidate>())
            {
                schedule.Validate(ChangeAction.Insert);
            }
            foreach (var reservation in Reservations.Cast<IValidate>())
            {
                reservation.Validate(ChangeAction.Insert);
            }
            return 1;
        }

        public void Dispose()
        {
        }
    }
}
