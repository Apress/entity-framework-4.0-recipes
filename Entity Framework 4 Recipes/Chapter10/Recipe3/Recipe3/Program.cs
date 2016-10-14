using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe3
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
                context.ExecuteStoreCommand("delete from chapter10.atmwithdrawal");
                context.ExecuteStoreCommand("delete from chapter10.atmmachine");
            }
        }

        static void RunExample()
        {
            DateTime today = DateTime.Parse("2/2/2010");
            DateTime yesterday = DateTime.Parse("2/1/2010");
            using (var context = new EFRecipesEntities())
            {
                var atm = new ATMMachine { ATMId = 17, Location = "12th and Main" };
                atm.ATMWithdrawals.Add(new ATMWithdrawal {Amount = 20.00M, Date= today});
                atm.ATMWithdrawals.Add(new ATMWithdrawal {Amount = 100.00M, Date = today});
                atm.ATMWithdrawals.Add(new ATMWithdrawal {Amount = 75.00M, Date = yesterday});
                atm.ATMWithdrawals.Add(new ATMWithdrawal {Amount = 50.00M, Date=  today});
                context.AddToATMMachines(atm);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var forToday = context.GetWithdrawls(17, today).FirstOrDefault();
                var forYesterday = context.GetWithdrawls(17, yesterday).FirstOrDefault();
                var atm = context.ATMMachines.Where(o => o.ATMId == 17).FirstOrDefault();
                Console.WriteLine("ATM Withdrawals for ATM at {0} at {1}", atm.ATMId.ToString(), atm.Location);
                Console.WriteLine("\t{0} Total Withdrawn = {1}", yesterday.ToShortDateString(), forYesterday.Value.ToString("C"));
                Console.WriteLine("\t{0} Total Withdrawn = {1}", today.ToShortDateString(), forToday.Value.ToString("C"));
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
