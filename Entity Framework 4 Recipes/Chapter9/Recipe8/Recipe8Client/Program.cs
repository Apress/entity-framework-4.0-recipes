using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipe8Client.ServiceReference1;

namespace Recipe8Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var serviceClient = new Service1Client())
            {
                serviceClient.InsertTestRecord();
                var client = serviceClient.GetClient();
                Console.WriteLine("Client is: {0} at {1}", client.Name, client.Email);
                client.Name = "Alex Park";
                client.Email = "AlexP@hotmail.com";
                serviceClient.Update(client);
                client = serviceClient.GetClient();
                Console.WriteLine("Client changed to: {0} at {1}", client.Name, client.Email);

                Console.WriteLine("Press <enter> to continue...");
                Console.ReadLine();
            }
        }
    }
}
