using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using ComplaintEntities;
namespace ComplaintService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void InsertTestRecord();

        [OperationContract]
        CustomerComplaint GetNextComplaint();

        [OperationContract]
        CustomerComplaint UpdateComplaint(CustomerComplaint complaint);
    }
}
