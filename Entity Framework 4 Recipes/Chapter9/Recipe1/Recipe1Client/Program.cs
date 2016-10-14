using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipe1Client.ServiceReference1;
namespace Recipe1Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Service1Client();
            var payment = client.InsertPayment();
            client.DeletePayment(payment);
        }
    }
}
