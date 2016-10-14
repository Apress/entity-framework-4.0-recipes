using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipe2Client.ServiceReference1;

namespace Recipe2Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service1Client();
            var order = service.InsertOrder();
            order.Quantity = 5;
            service.UpdateOrderWithoutRetrieving(order);
            order = service.InsertOrder();
            order.Quantity = 3;
            service.UpdateOrderByRetrieving(order);
        }
    }
}
