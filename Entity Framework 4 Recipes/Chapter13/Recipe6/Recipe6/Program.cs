using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Diagnostics;

namespace Recipe6
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
                context.ExecuteStoreCommand("delete from chapter13.payment");
                context.ExecuteStoreCommand("delete from chapter13.account");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 5000; i++)
                {
                    var account = context.CreateObject<Account>();
                    account.Name = "Test" + i.ToString();
                    account.Balance = 10M;
                    account.Payments.Add(new Payment { PaidTo = "Test" + (i + 1).ToString(), Paid = 5M });
                    context.Accounts.AddObject(account);
                }
                context.SaveChanges();
                watch.Stop();
                Console.WriteLine("Time to insert: {0} seconds", watch.Elapsed.TotalSeconds.ToString());
            }

            using (var context = new EFRecipesEntities())
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var accounts = context.Accounts.Include("Payments").ToList();
                watch.Stop();
                Console.WriteLine("Time to read: {0} seconds", watch.Elapsed.TotalSeconds.ToString());

                watch.Restart();
                foreach (var account in accounts)
                {
                    account.Balance += 10M;
                    account.Payments.First().Paid += 1M;
                }
                context.SaveChanges();
                watch.Stop();
                Console.WriteLine("Time to update: {0} seconds", watch.Elapsed.TotalSeconds.ToString());
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }

    public class Account
    {
        public virtual int AccountId { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

    public class Payment
    {
        public virtual int PaymentId { get; set; }
        public virtual string Name { get; set; }
        public virtual string PaidTo { get; set; }
        public virtual decimal Paid { get; set; }
        public virtual int AccountId { get; set; }
    }

    public class EFRecipesEntities : ObjectContext
    {
        private ObjectSet<Account> _accounts;
        private ObjectSet<Payment> _payments;

        public EFRecipesEntities()
            : base("name=EFRecipesEntities", "EFRecipesEntities")
        {
            _accounts = CreateObjectSet<Account>();
            _payments = CreateObjectSet<Payment>();
        }

        public ObjectSet<Account> Accounts
        {
            get { return _accounts; }
        }

        public ObjectSet<Payment> Payments
        {
            get { return _payments; }
        }
    }
}
