using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe9
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
                context.ExecuteStoreCommand("delete from chapter11.movierental");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var mr1 = new MovieRental { Title = "A Day in the Life", RentalDate = DateTime.Parse("2/19/2010"), ReturnedDate = DateTime.Parse("3/4/2010"),LateFees = 3M };
                var mr2 = new MovieRental { Title = "The Shortest Yard", RentalDate = DateTime.Parse("3/15/2010"), ReturnedDate = DateTime.Parse("3/20/2010"), LateFees = 0M };
                var mr3 = new MovieRental { Title = "Jim's Story", RentalDate = DateTime.Parse("3/2/2010"), ReturnedDate = DateTime.Parse("3/19/2010"), LateFees = 3M };
                context.MovieRentals.AddObject(mr1);
                context.MovieRentals.AddObject(mr2);
                context.MovieRentals.AddObject(mr3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Movie rentals late returns");
                Console.WriteLine("==========================");
                var late = from r in context.MovieRentals
                           where EntityFunctions.DiffDays(r.RentalDate, r.ReturnedDate) > 10
                           select r;
                foreach (var rental in late)
                {
                    Console.WriteLine("{0} was {1} days late, fee: {2}", rental.Title, (rental.ReturnedDate - rental.RentalDate).Days - 10, rental.LateFees.ToString("C"));
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        
        }
    }
}
