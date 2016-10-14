using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Recipe2
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
                context.ExecuteStoreCommand("delete from chapter10.rental");
                context.ExecuteStoreCommand("delete from chapter10.vehicle");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var car1 = new Vehicle { Manufacturer = "Toyota", Model = "Camry", Year = 2010 };
                var car2 = new Vehicle { Manufacturer = "Chevrolet", Model = "Corvette", Year = 2010 };
                var r1 = new Rental { Vehicle = car1, RentalDate = DateTime.Parse("2/2/2010"), Payment = 59.95M };
                var r2 = new Rental { Vehicle = car2, RentalDate = DateTime.Parse("2/2/2010"), Payment = 139.95M };
                context.AddToRentals(r1);
                context.AddToRentals(r2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())       
            {
                string reportDate = "2/2/2010";
                var totalRentals = new ObjectParameter("TotalRentals", typeof(int));
                var totalPayments = new ObjectParameter("TotalPayments", typeof(decimal));
                var vehicles = context.GetVehiclesWithRentals(DateTime.Parse(reportDate), totalRentals, totalPayments);
                Console.WriteLine("Rental Activity for {0}",reportDate);
                Console.WriteLine("Vehicles Rented");
                foreach(var vehicle in vehicles)
                {
                    Console.WriteLine("{0} {1} {2}", vehicle.Year.ToString(), vehicle.Manufacturer, vehicle.Model);
                }
                Console.WriteLine("Total Rentals: {0}", ((int)totalRentals.Value).ToString());
                Console.WriteLine("Total Payments: {0}", ((decimal)totalPayments.Value).ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
