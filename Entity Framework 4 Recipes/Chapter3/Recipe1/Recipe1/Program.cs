using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace Recipe1
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
                context.ExecuteStoreCommand("delete from chapter3.payment");
            }
        }

        static void RunExample()
        {
            // insert a couple rows
            using (var context = new EFRecipesEntities())
            {
                string sql = @"insert into Chapter3.Payment(Amount, Vendor)
                               values (@Amount, @Vendor)";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "Amount", Value = 99.97M},
                    new SqlParameter { ParameterName = "Vendor", Value="Ace Plumbing"}
                };
                int rowCount = context.ExecuteStoreCommand(sql, args);

                args = new DbParameter[] {
                    new SqlParameter { ParameterName = "Amount", Value = 43.83M},
                    new SqlParameter { ParameterName = "Vendor", Value = "Joe's Trash Service"}
                };
                rowCount += context.ExecuteStoreCommand(sql, args);
                Console.WriteLine("{0} rows inserted", rowCount.ToString());
            }

            // materialize some entities
            using (var context = new EFRecipesEntities())
            {
                Console.WriteLine("Payments");
                Console.WriteLine("========");
                foreach (var payment in context.Payments)
                {
                    Console.WriteLine("Paid {0} to {1}", payment.Amount.ToString("C"), payment.Vendor);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
