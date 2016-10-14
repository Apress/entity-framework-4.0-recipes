using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;
using ComplaintEntities;
using ComplaintData;
namespace ComplaintService
{
    public class Service1 : IService1
    {
        public void InsertTestRecord()
        {
            using (var context = new EFRecipesEntities())
            {
                context.ExecuteStoreCommand("delete from chapter13.customercomplaint");
                var complaint = new CustomerComplaint { Comment = "Your store should open early on Saturdays", ReportedBy = "Jill Morgan" };
                context.CustomerComplaints.AddObject(complaint);
                context.SaveChanges();
            }
        }

        public CustomerComplaint GetNextComplaint()
        {
            using (var context = new EFRecipesEntities())
            {
                var complaint = context.CustomerComplaints.Where(c => c.ActionTaken == null).First();
                complaint.StartTracking();
                return complaint;
            }
        }

        public CustomerComplaint UpdateComplaint(CustomerComplaint complaint)
        {
            using (var context = new EFRecipesEntities())
            {
                context.CustomerComplaints.ApplyChanges(complaint);
                context.SaveChanges();
                complaint.AcceptChanges();
                return complaint;
            }
        }
    }
}
