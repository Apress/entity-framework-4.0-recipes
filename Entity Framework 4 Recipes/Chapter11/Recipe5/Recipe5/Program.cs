using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using System.Data.EntityClient;

namespace Recipe5
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
                context.ExecuteStoreCommand("delete from chapter11.reservation");
                context.ExecuteStoreCommand("delete from chapter11.visitor");
                context.ExecuteStoreCommand("delete from chapter11.hotel");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var hotel = new Hotel { Name = "Five Seasons Resort" };
                var v1 = new Visitor { Name = "Alex Stevens" };
                var v2 = new Visitor { Name = "Joan Hills" };
                var r1 = new Reservation { Cost = 79.99M, Hotel = hotel, ReservationDate = DateTime.Parse("2/19/2010"), Visitor = v1 };
                var r2 = new Reservation { Cost = 99.99M, Hotel = hotel, ReservationDate = DateTime.Parse("2/17/2010"), Visitor = v2 };
                var r3 = new Reservation { Cost = 109.99M, Hotel = hotel, ReservationDate = DateTime.Parse("2/18/2010"), Visitor = v1 };
                var r4 = new Reservation { Cost = 89.99M, Hotel = hotel, ReservationDate = DateTime.Parse("2/17/2010"), Visitor = v2 };
                context.Hotels.AddObject(hotel);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            { 
                Console.WriteLine("Using eSql...");
                var esql = @"Select value v from EFRecipesModel.VisitorSummary(DATETIME'2010-02-16 00:00', 7) as v";
                var visitors = context.CreateQuery<DbDataRecord>(esql);
                foreach (var visitor in visitors)
                {
                    Console.WriteLine("{0}, Total Reservations: {1}, Revenue: {2:C}",
                        visitor["Name"], visitor["TotalReservations"], visitor["BusinessEarned"]);
                }
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine();
                Console.WriteLine("Using LINQ...");
                var visitors = from v in context.VisitorSummary(DateTime.Parse("2/16/2010"), 7)
                               select v;
                foreach (var visitor in visitors)
                {
                    Console.WriteLine("{0}, Total Reservations: {1}, Revenue: {2:C}",
                        visitor["Name"], visitor["TotalReservations"], visitor["BusinessEarned"]);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    partial class EFRecipesEntities
    {
        [EdmFunction("EFRecipesModel", "VisitorSummary")]
        public IQueryable<DbDataRecord> VisitorSummary(DateTime StartDate, int Days)
        {
            return this.QueryProvider.CreateQuery<DbDataRecord>(
                Expression.Call(
                Expression.Constant(this),
                (MethodInfo)MethodInfo.GetCurrentMethod(),
                new Expression[] { Expression.Constant(StartDate), Expression.Constant(Days) }
                ));
        }
    }
}
