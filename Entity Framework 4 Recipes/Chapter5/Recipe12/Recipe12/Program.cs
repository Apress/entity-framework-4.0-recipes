using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recipe12
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
                context.ExecuteStoreCommand("delete from chapter5.invoice");
                context.ExecuteStoreCommand("delete from chapter5.client");
            }
        }

        static void RunExample()
        {
            using (var context = new EFRecipesEntities())
            {
                var client1 = new Client { Name = "Karen Standfield", ClientId = 1 };
                var invoice1 = new Invoice { InvoiceDate = DateTime.Parse("4/1/10"), Amount = 29.95M };
                var invoice2 = new Invoice { InvoiceDate = DateTime.Parse("4/2/10"), Amount = 49.95M };
                var invoice3 = new Invoice { InvoiceDate = DateTime.Parse("4/3/10"), Amount = 102.95M };
                var invoice4 = new Invoice { InvoiceDate = DateTime.Parse("4/4/10"), Amount = 45.99M };

                // add the invoice
                // to the client's collection
                client1.Invoices.Add(invoice1);

                // assign the foreign key
                // directly
                invoice2.ClientId = 1;

                // Attach() and existing
                // row using a "fake" entity
                context.ExecuteStoreCommand("insert into chapter5.client values (2, 'Phil Marlowe')");
                var client2 = new Client { ClientId = 2 };
                context.Clients.Attach(client2);
                invoice3.Client = client2;

                // using the ClientReference
                invoice4.ClientReference.Value = client1;

                // save the changes
                context.Clients.AddObject(client1);
                context.Invoices.AddObject(invoice2);
                context.Invoices.AddObject(invoice3);
                context.SaveChanges();
            }

            using (var context = new EFRecipesEntities())
            {
                foreach (var client in context.Clients)
                {
                    Console.WriteLine("Client: {0}", client.Name);
                    foreach (var invoice in client.Invoices)
                    {
                        Console.WriteLine("\t{0} for {1}", invoice.InvoiceDate.ToShortDateString(), invoice.Amount.ToString("C"));
                    }
                }
            }

            Console.WriteLine("Press <enter> to continue...");
            Console.ReadLine();
        }
    }
}
