using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                context.ExecuteStoreCommand("delete from chapter2.account");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("insert into chapter2.account (DeletedOn,AccountHolderId) values ('2/10/2009',1728)");

                var account = new Account { AccountHolderId = 2320 };
                context.Accounts.AddObject(account);
                account = new Account { AccountHolderId = 2502 };
                context.Accounts.AddObject(account);
                account = new Account { AccountHolderId = 2603 };
                context.Accounts.AddObject(account);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var account in context.Accounts)
                {
                    Console.WriteLine("Account Id = {0}", account.AccountHolderId.ToString());
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
