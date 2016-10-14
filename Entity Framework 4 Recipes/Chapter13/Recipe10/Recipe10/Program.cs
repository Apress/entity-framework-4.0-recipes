using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe10
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
                context.ExecuteStoreCommand("delete from chapter13.[transaction]");
                context.ExecuteStoreCommand("delete from chapter13.creditcard");
                context.ExecuteStoreCommand("delete from chapter13.customer");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var cust1 = new Customer { Name = "Robin Rosen", City = "Raytown" };
                var card1 = new CreditCard { CardNumber = "41949494338899", ExpirationDate = DateTime.Parse("12/2010"), Type = "Visa" };
                var trans1 = new Transaction { Amount = 29.95M };
                card1.Transactions.Add(trans1);
                cust1.CreditCards.Add(card1);
                var cust2 = new Customer { Name = "Bill Meyers", City = "Raytown" };
                var card2 = new CreditCard { CardNumber = "41238389484448", ExpirationDate = DateTime.Parse("12/2013"), Type = "Visa" };
                var trans2 = new Transaction { Amount = 83.39M };
                card2.Transactions.Add(trans2);
                cust2.CreditCards.Add(card2);
                context.Customers.AddObject(cust1);
                context.Customers.AddObject(cust2);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                var customers = context.Customers.Where(c => c.City == "Raytown");
                var creditCards = customers.SelectMany(c => c.CreditCards);
                var transactions = creditCards.SelectMany(cr => cr.Transactions);

                // execute queries, EF fixes up associations
                customers.ToList();
                creditCards.ToList();
                transactions.ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine("Customer: {0} in {1}", customer.Name, customer.City);
                    foreach (var creditCard in customer.CreditCards)
                    {
                        Console.WriteLine("\tCard: {0} expires on {1}", creditCard.CardNumber, creditCard.ExpirationDate.ToShortDateString());
                        foreach (var trans in creditCard.Transactions)
                        {
                            Console.WriteLine("\t\tTransaction: {0}", trans.Amount.ToString("C"));
                        }
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
