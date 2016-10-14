using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
                context.ExecuteStoreCommand("delete from chapter14.account");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.Accounts.AddObject(new Account { AccountNumber = "8675309",  Balance = 100M, Name = "Robin Rosen"});
                context.Accounts.AddObject(new Account { AccountNumber = "8535937", Balance = 25M, Name = "Steven Bishop"});
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                // get the account
                var account = context.Accounts.First(a => a.AccountNumber == "8675309");
                Console.WriteLine("Account for {0}", account.Name);
                Console.WriteLine("\tPrevious Balance: {0}", account.Balance.ToString("C"));

                // some other process updates the balance
                Console.WriteLine("[Rogue process updates balance!]");
                context.ExecuteStoreCommand("update chapter14.account set balance = 1000 where accountnumber = '8675309'");

                // update the account balance
                account.Balance = 10M;

                try
                {
                    Console.WriteLine("\tNew Balance: {0}", account.Balance.ToString("C"));
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
