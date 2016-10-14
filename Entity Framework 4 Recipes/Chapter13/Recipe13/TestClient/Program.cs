using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplaintEntities;
using TestClient.ServiceReference1;
namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new Service1Client())
            {
                // insert a test record
                client.InsertTestRecord();

                var next = client.GetNextComplaint();
                next.ActionTaken = "Your issue is being reviewed";
                client.UpdateComplaint(next);
            }
        }
    }
}
